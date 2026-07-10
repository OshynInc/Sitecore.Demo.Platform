# 10 — Serialization & Deploy

Deploy the NYSIF demo via CLI. Use **Content Editor** to verify and troubleshoot after deploy.

> **Content Editor:** [00 — Content Editor basics](./00-content-editor-basics.md)

## One-command deploy

```bash
cd Demo
.\up.ps1
```

`up.ps1`:

1. Starts Docker / Sitecore
2. Runs `build-themes.ps1 -Themes @("NYSIF")`
3. Runs `dotnet sitecore ser push`
4. Publishes and rebuilds indexes
5. Opens CM, CD, and `https://cd.lighthouse.localhost/nysif`

## Modules pushed

| Module | Content |
|--------|---------|
| `Project.NYSIF.Website` | Site, data, presentation, partial designs |
| `Feature.ExperienceAccelerator.Extensions` | Custom renderings + templates |
| `Project.Global.Website` | NYSIF theme media blobs |

## Manual deploy

```bash
.\build-themes.ps1 -Themes @("NYSIF")
dotnet sitecore ser push
dotnet sitecore publish
```

## Pull CM changes back

```bash
dotnet sitecore ser pull -n Project.NYSIF.Website
```

## Site URL

- **CD:** `https://cd.lighthouse.localhost/nysif`
- **Site Grouping:** empty `HostName`, `VirtualFolder: /nysif` ([01](./01-create-nysif-site.md))

---

## Rendering path reference

Paste paths into the Content Editor **tree jump box** (**master** database). Use **Layout Details** on a partial design, page design, or page to add controls by rendering.

### Page structure

| Component | Rendering path |
|-----------|----------------|
| Container | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Structure/Container` |
| Column Splitter | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Structure/Column Splitter` |
| Row Splitter | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Structure/Row Splitter` |

### Media & navigation

| Component | Rendering path |
|-----------|----------------|
| Image | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Media/Image` |
| Navigation | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Navigation/Navigation` |
| Link | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Navigation/Link` |
| Link List | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Navigation/Link List` |

### Page content (SXA)

| Component | Rendering path |
|-----------|----------------|
| Promo | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Content/Promo` |
| Page List | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Content/Page List` |
| Rich Text | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Content/Rich Text` |
| Page Content | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Content/Page Content` |

### NYSIF custom renderings

| Component | Rendering path | Item ID (Hero) |
|-----------|----------------|----------------|
| Hero | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Hero` | `{6BCF8D8E-0A28-4844-BFB4-96C852BAD2DC}` |
| Service Alerts | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Service Alerts` | — |
| Role Cards | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Role Cards` | — |
| Testimonial | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Testimonial` | — |

Tree folder for custom renderings: **Feature → Demo Shared → SitecoreDemo XA Extensions**.

If custom renderings are missing:

```bash
dotnet sitecore ser push -i Feature.ExperienceAccelerator.Extensions
```

**Available Renderings** item (registers custom renderings for layout pickers):

`/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Available Renderings/SitecoreDemo`

### Key content items

| Item | Path |
|------|------|
| Home | `/sitecore/content/Demo SXA Sites/NYSIF/Home` |
| Page design | `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Page Designs/NYSIF Homepage` |
| Header partial | `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Partial Designs/NYSIF Header` |
| Footer partial | `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Partial Designs/NYSIF Footer` |
| Data root | `/sitecore/content/Demo SXA Sites/NYSIF/Data` |
| Theme | `/sitecore/media library/Demo SXA Sites/NYSIF` |
| Site Settings | `/sitecore/content/Demo SXA Sites/NYSIF/Settings` |

## Guide index

| Doc | Topic |
|-----|-------|
| [00](./00-content-editor-basics.md) | Content Editor conventions |
| [01](./01-create-nysif-site.md) | Site + Site Grouping |
| [02](./02-create-nysif-theme.md) | Theme assignment |
| [03](./03-partial-designs-header-footer.md) | Header / footer Layout Details |
| [04](./04-page-design-homepage.md) | Homepage page design |
| [05](./05-navigation-mega-menu.md) | Navigation |
| [06](./06-hero-and-alerts.md) | Hero + alerts |
| [07](./07-content-sections.md) | Body sections |
| [08](./08-compose-homepage.md) | Publish + verify |
| [09](./09-theme-dev-workflow.md) | FrontEnd theme dev |
