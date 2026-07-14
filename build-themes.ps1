[CmdletBinding()]
Param(
  [string]$FrontEndPath = "FrontEnd",
  [string]$ItemsPath = "src\items\Project.Global.Website.Themes\Demo SXA Sites",
  [string[]]$Themes = @()
)

$DemoRoot = $PSScriptRoot
if (-not [System.IO.Path]::IsPathRooted($FrontEndPath)) {
  $FrontEndPath = Join-Path $DemoRoot $FrontEndPath
}
if (-not [System.IO.Path]::IsPathRooted($ItemsPath)) {
  $ItemsPath = Join-Path $DemoRoot $ItemsPath
}

$DartOnlyThemes = @("NYSIF")

function Update-ThemeBlobItem {
  param(
    [string]$FilePath,
    [string]$TemplatePath,
    [string]$OutputPath
  )

  if (-not (Test-Path $FilePath)) {
    Write-Warning "Build output not found: $FilePath"
    return $false
  }
  if (-not (Test-Path $TemplatePath)) {
    Write-Warning "Template not found: $TemplatePath"
    return $false
  }

  Write-Host "Embedding theme blob: $OutputPath" -ForegroundColor Cyan
  $blob = [Convert]::ToBase64String([IO.File]::ReadAllBytes($FilePath))
  $size = (Get-Item $FilePath).Length
  # New BlobID each build so Sitecore Serialization detects media changes
  $blobId = [guid]::NewGuid().ToString()
  $content = Get-Content -Path $TemplatePath -Raw
  $content = $content -replace '%blob%', $blob
  $content = $content -replace '%size%', $size
  $content = $content -replace 'BlobID: "[^"]+"', "BlobID: `"$blobId`""
  $content = $content -replace '(Hint: __Revision\r?\n\s+Value: ")[^"]+(")', "`${1}theme-blob-$blobId`${2}"
  $content | Set-Content -Path $OutputPath -Encoding UTF8 -NoNewline
  return $true
}

function Build-ThemeWithGulp {
  param([string]$ThemeRoot)

  Push-Location $ThemeRoot
  try {
    if (-not (Test-Path "node_modules")) {
      Write-Host "  npm install..." -ForegroundColor Yellow
      npm --loglevel=error install
      if ($LASTEXITCODE -ne 0) {
        throw "npm install failed"
      }
    }

    npm --loglevel=error run build
    if ($LASTEXITCODE -ne 0) {
      throw "npm run build failed"
    }
  }
  finally {
    Pop-Location
  }
}

$DartSassVersion = "1.77.0"

function Invoke-DartSass {
  param(
    [string]$InputFile,
    [string]$OutputFile,
    [string[]]$LoadPaths
  )

  $sassArgs = @(
    "sass@$DartSassVersion",
    $InputFile,
    $OutputFile,
    "--style=expanded",
    "--no-source-map"
  ) + $LoadPaths

  $prevEap = $ErrorActionPreference
  $prevNativePref = $null
  if (Get-Variable -Name PSNativeCommandUseErrorActionPreference -ErrorAction SilentlyContinue) {
    $prevNativePref = $PSNativeCommandUseErrorActionPreference
    $PSNativeCommandUseErrorActionPreference = $false
  }
  $ErrorActionPreference = "Continue"
  $sassOutput = @()
  try {
    $sassOutput = & npx --yes @sassArgs 2>&1
    $sassExit = $LASTEXITCODE
  }
  finally {
    $ErrorActionPreference = $prevEap
    if ($null -ne $prevNativePref) {
      $PSNativeCommandUseErrorActionPreference = $prevNativePref
    }
  }

  if ($sassExit -ne 0) {
    $message = ($sassOutput | Out-String).Trim()
    if ($message) {
      Write-Warning $message
    }
  }

  return $sassExit
}

function Build-ThemeWithDartSass {
  param([string]$ThemeRoot)

  Write-Host "  Using Dart Sass fallback (no gulp/node-sass)..." -ForegroundColor Yellow

  $stylesDir = Join-Path $ThemeRoot "styles"
  $scriptsDir = Join-Path $ThemeRoot "scripts"
  if (-not (Test-Path $stylesDir)) { New-Item -ItemType Directory -Path $stylesDir | Out-Null }
  if (-not (Test-Path $scriptsDir)) { New-Item -ItemType Directory -Path $scriptsDir | Out-Null }

  $cssOut = Join-Path $stylesDir "pre-optimized-min.css"
  $sassRoot = Join-Path $ThemeRoot "sass"
  $loadPaths = @(
    "--load-path=$sassRoot",
    "--load-path=$(Join-Path $sassRoot 'abstracts')",
    "--load-path=$(Join-Path $sassRoot 'base')",
    "--load-path=$(Join-Path $sassRoot 'layout')"
  )

  $coreComponents = @(
    "component-navigation.scss",
    "component-hero.scss",
    "component-service-alerts.scss",
    "component-role-cards.scss",
    "component-testimonial.scss",
    "component-promo.scss",
    "component-page-list.scss",
    "component-link-list.scss"
  )

  $scssFiles = @()
  foreach ($name in $coreComponents) {
    $path = Join-Path $sassRoot $name
    if (Test-Path $path) { $scssFiles += Get-Item $path }
  }
  $mainScss = Join-Path $sassRoot "main.scss"
  if (Test-Path $mainScss) { $scssFiles += Get-Item $mainScss }
  $scssFiles += Get-ChildItem (Join-Path $sassRoot "styles") -Recurse -Filter "*.scss" -ErrorAction SilentlyContinue |
    Where-Object { $_.Name -like "nysif-*" -or $_.Name -like "row-*" }
  $scssFiles += Get-ChildItem (Join-Path $sassRoot "variants") -Recurse -Filter "nysif-*.scss" -ErrorAction SilentlyContinue

  if ($scssFiles.Count -eq 0) {
    throw "No SCSS sources found under $sassRoot"
  }

  $cssParts = New-Object System.Collections.Generic.List[string]
  $failedFiles = New-Object System.Collections.Generic.List[string]
  foreach ($scssFile in ($scssFiles | Sort-Object FullName -Unique)) {
    $tempCss = [System.IO.Path]::ChangeExtension([System.IO.Path]::GetTempFileName(), ".css")
    $sassExit = Invoke-DartSass -InputFile $scssFile.FullName -OutputFile $tempCss -LoadPaths $loadPaths
    if ($sassExit -ne 0) {
      Write-Warning "Sass compile failed: $($scssFile.Name)"
      $failedFiles.Add($scssFile.Name) | Out-Null
      Remove-Item $tempCss -ErrorAction SilentlyContinue
      continue
    }
    $cssParts.Add([IO.File]::ReadAllText($tempCss))
    Remove-Item $tempCss -ErrorAction SilentlyContinue
  }

  if ($failedFiles.Count -gt 0) {
    throw "Sass compile failed for: $($failedFiles -join ', ')"
  }

  # Browsers ignore @import unless it is the first rule — prepend Montserrat here
  $fontImport = '@import url("https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700;800&display=swap");'
  $joined = ($cssParts -join "`n") -replace '(?m)^@import url\("https://fonts\.googleapis\.com[^"]+"\);\r?\n?', ''
  [IO.File]::WriteAllText($cssOut, "$fontImport`n$joined")

  $jsOut = Join-Path $scriptsDir "pre-optimized-min.js"
  $jsFiles = Get-ChildItem $scriptsDir -Filter "*.js" |
    Where-Object { $_.Name -ne "pre-optimized-min.js" -and $_.Name -like "component-*nysif*" } |
    Sort-Object Name

  if ($jsFiles.Count -eq 0) {
    $jsFiles = Get-ChildItem $scriptsDir -Filter "component-navigation-nysif.js" -ErrorAction SilentlyContinue
  }

  if ($jsFiles.Count -eq 0) {
    [IO.File]::WriteAllText($jsOut, "")
  }
  else {
    $jsParts = $jsFiles | ForEach-Object { [IO.File]::ReadAllText($_.FullName) }
    [IO.File]::WriteAllText($jsOut, ($jsParts -join "`n"))
  }
}

function Build-ThemeAssets {
  param(
    [string]$ThemeRoot,
    [switch]$UseDartSassOnly
  )

  if ($UseDartSassOnly) {
    Build-ThemeWithDartSass -ThemeRoot $ThemeRoot
  }
  else {
    try {
      Build-ThemeWithGulp -ThemeRoot $ThemeRoot
    }
    catch {
      Write-Warning "Gulp build failed ($($_.Exception.Message)); falling back to Dart Sass."
      Build-ThemeWithDartSass -ThemeRoot $ThemeRoot
    }
  }

  $css = Join-Path $ThemeRoot "styles\pre-optimized-min.css"
  $js = Join-Path $ThemeRoot "scripts\pre-optimized-min.js"
  if (-not (Test-Path $css) -or -not (Test-Path $js)) {
    throw "Theme build did not produce pre-optimized-min.css/js"
  }
}

$themeDirs = Get-ChildItem $FrontEndPath -Depth 0 -Directory
if ($Themes.Count -gt 0) {
  $themeDirs = $themeDirs | Where-Object { $Themes -contains $_.Name }
}

foreach ($themeDir in $themeDirs) {
  $themeName = $themeDir.Name
  $themeRoot = (Join-Path $FrontEndPath $themeName)
  $itemRoot = Join-Path $ItemsPath $themeName
  $useDartOnly = $DartOnlyThemes -contains $themeName

  Write-Host "Building theme: $themeName" -ForegroundColor Green
  if ($useDartOnly) {
    Write-Host "  Using Dart Sass (NYSIF theme; gulp/node-sass not required)." -ForegroundColor Cyan
  }
  Build-ThemeAssets -ThemeRoot $themeRoot -UseDartSassOnly:$useDartOnly

  $cssOk = Update-ThemeBlobItem `
    -FilePath (Join-Path $themeRoot "styles\pre-optimized-min.css") `
    -TemplatePath (Join-Path $itemRoot "styles\pre-optimized-min.yml.template") `
    -OutputPath (Join-Path $itemRoot "styles\pre-optimized-min.yml")

  $jsOk = Update-ThemeBlobItem `
    -FilePath (Join-Path $themeRoot "scripts\pre-optimized-min.js") `
    -TemplatePath (Join-Path $itemRoot "scripts\pre-optimized-min.yml.template") `
    -OutputPath (Join-Path $itemRoot "scripts\pre-optimized-min.yml")

  if (-not $cssOk -or -not $jsOk) {
    throw "Failed to embed theme blobs for $themeName"
  }
}

Write-Host "Theme build complete." -ForegroundColor Green
