# 01 — Create NYSIF Site

Create the NYSIF SXA site under the Demo tenant.

> **Content Editor:** [00 — Content Editor basics](./00-content-editor-basics.md) · Use **master** database · paste paths into the tree jump box.

## Option A: Deploy serialized content (recommended)

1. From `Demo/`, run:
   ```bash
   dotnet sitecore ser push -n Project.NYSIF.Website
   dotnet sitecore ser push -n Project.Global.Website
   dotnet sitecore publish
   ```
2. In Content Editor, jump to `/sitecore/content/Demo SXA Sites/NYSIF` and confirm the site tree exists.

## Option B: SXA site wizard (manual)

1. Content Editor → `/sitecore/content/Demo SXA Sites`.
2. Right-click → **Insert** → **Site** (SXA site wizard).
3. Configure:
   - **Site name:** `NYSIF`
   - **Tenant:** Demo SXA Sites
   - **Shared sites:** Global
   - **Theme:** NYSIF (`/sitecore/media library/Demo SXA Sites/NYSIF`)
4. Finish the wizard. Note the **Start item** path (typically `/sitecore/content/Demo SXA Sites/NYSIF/Home`).

## Site Grouping (Content Editor)

**Item:** `/sitecore/content/Demo SXA Sites/NYSIF/Settings/Site Grouping/NYSIF`

Open the item → **Content** tab → set fields → **Save**:

| Field (Content tab) | Value |
|---------------------|-------|
| SiteName | `NYSIF` |
| HostName | *(empty — uses shared CD host)* |
| VirtualFolder | `/nysif` |
| StartItem | `{d1e0f9a8-b7c6-4543-6543-210987fedcba}` (Home) |
| OtherProperties | `dictionaryPath=/sitecore/content/Demo SXA Sites/Global/Data/Dictionary` |

To pick **StartItem**: click the field → select `/sitecore/content/Demo SXA Sites/NYSIF/Home`.

## Available renderings

Custom NYSIF renderings must be listed before they appear in layout pickers. **Item:**

`/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Available Renderings/SitecoreDemo`

**Content** tab → **Renderings** field should include Hero, Service Alerts, Role Cards, Testimonial (GUIDs listed in serialization). After deploy this is pre-filled.

| Rendering | Path |
|-----------|------|
| Hero | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Hero` |
| Service Alerts | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Service Alerts` |
| Role Cards | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Role Cards` |
| Testimonial | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Testimonial` |

Standard SXA renderings (Container, Promo, Link List, etc.) come from the **Global** shared site. Full list: [10-serialization-deploy.md](./10-serialization-deploy.md).

## Verify (Content Editor + CD)

| Check | How |
|-------|-----|
| Site tree | Jump to `/sitecore/content/Demo SXA Sites/NYSIF` |
| Compatible themes | Item `/sitecore/content/Demo SXA Sites/NYSIF/Settings` → **Compatible themes** includes NYSIF theme ([02](./02-create-nysif-theme.md)) |
| CD URL | Browse `https://cd.lighthouse.localhost/nysif` after publish |

> Screenshot placeholder: Site Grouping item with HostName empty and VirtualFolder `/nysif`.
