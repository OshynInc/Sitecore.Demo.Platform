param(
    [Parameter(Mandatory)]
    [string]$SolrConfigPath,

    [Parameter(Mandatory)]
    [string]$XsltPath,

    [Parameter(Mandatory)]
    [string]$OutputPath
)

function Convert-Xslt {
    param(
        [string]$SrcXmlPath,
        [string]$XlstPath,
        [string]$OutputPath
    )
    $XsltSettings = New-Object System.Xml.Xsl.XsltSettings($true, $false);
    $XSLInputElement = New-Object System.Xml.Xsl.XslCompiledTransform;
    $XmlUrlResolver = New-Object System.Xml.XmlUrlResolver;
    $XSLInputElement.Load($XlstPath, $XsltSettings, $XmlUrlResolver);
    $XSLInputElement.Transform($SrcXmlPath, $OutputPath)
}

function Resolve-SchemaSourcePath {
    param(
        [string]$SolrConfigPath,
        [string]$XmlFileName
    )

    $srcXmlPath = Join-Path -Path $SolrConfigPath -ChildPath $XmlFileName
    if (Test-Path $srcXmlPath) {
        return $srcXmlPath
    }

    # Solr 8.11+ downloads the default configset schema as "managed-schema" (no extension).
    if ($XmlFileName -eq 'managed-schema.xml') {
        $legacySchemaPath = Join-Path -Path $SolrConfigPath -ChildPath 'managed-schema'
        if (Test-Path $legacySchemaPath) {
            Write-Host "Copying '$legacySchemaPath' to '$srcXmlPath' for XSLT transformation"
            Copy-Item -Path $legacySchemaPath -Destination $srcXmlPath -Force
            return $srcXmlPath
        }
    }

    return $srcXmlPath
}

Copy-Item -Path $SolrConfigPath -Destination "$OutputPath" -Recurse -Force

$include = @("managed-schema.*", "solrconfig.*")
$xsltFiles = Get-ChildItem -Path $XsltPath -Include $include -Recurse

foreach ($xsltFile in $xsltFiles) {
    $xsltFilePath = $xsltFile.FullName
    $xsltFileName = Split-Path $xsltFilePath -Leaf
    $xmlFileName = [io.path]::GetFileNameWithoutExtension($xsltFilePath)
    Write-Host "Transformation '$OutputPath\$xmlFileName' file based on '$SolrConfigPath' Solr config with using '$xsltFileName' xslt file"
    $srcXmlPath = Resolve-SchemaSourcePath -SolrConfigPath $SolrConfigPath -XmlFileName $xmlFileName
    $outputXmlPath = Join-Path -Path $OutputPath -ChildPath $xmlFileName
    Convert-Xslt -SrcXmlPath $srcXmlPath -XlstPath $xsltFilePath -OutputPath $outputXmlPath

    # Solr 8.11 reads "managed-schema" (no extension). The XSLT writes "managed-schema.xml",
    # so replace the copied default schema with the transformed Sitecore schema.
    if ($xmlFileName -eq 'managed-schema.xml' -and (Test-Path $outputXmlPath)) {
        $activeSchemaPath = Join-Path -Path $OutputPath -ChildPath 'managed-schema'
        Write-Host "Applying transformed schema to '$activeSchemaPath'"
        Copy-Item -Path $outputXmlPath -Destination $activeSchemaPath -Force
        Remove-Item -Path $outputXmlPath -Force
    }
}
