# 02 — Create NYSIF Theme

The NYSIF theme lives in the repo and maps to the Media Library.

> **Content Editor:** [00 — Content Editor basics](./00-content-editor-basics.md) · **master** database.

## Media Library theme item

**Jump to:** `/sitecore/media library/Demo SXA Sites/NYSIF`

| Field | Value |
|-------|-------|
| Template | SXA Theme |
| BaseLayout | Core Libraries, Main, Components, Grid (same as Lighthouse) |
| Item ID | `{A10532BC-6FE9-4B72-82C1-4ED80599A86B}` |

Serialized file: `Demo/src/items/Project.Global.Website.Themes/Demo SXA Sites/NYSIF.yml`

After `build-themes.ps1` / deploy, compiled assets appear under:

| Folder | Path |
|--------|------|
| Styles | `/sitecore/media library/Demo SXA Sites/NYSIF/styles` |
| Scripts | `/sitecore/media library/Demo SXA Sites/NYSIF/scripts` |

In Content Editor, expand the theme item in the tree to verify CSS/JS media children exist.

## Assign theme to site (Content Editor)

**Important:** **Compatible themes** is on **Site Settings** — not on Site Grouping.

| Open this item | `/sitecore/content/Demo SXA Sites/NYSIF/Settings` |
|----------------|-----------------------------------------------------|
| **Not** this item | `/sitecore/content/Demo SXA Sites/NYSIF/Settings/Site Grouping/NYSIF` |

Steps:

1. Jump to `/sitecore/content/Demo SXA Sites/NYSIF/Settings`.
2. **Content** tab → **Compatible themes** (serialized field name: `Themes`).
3. If missing, enable **View → Show Standard Fields**.
4. Add `/sitecore/media library/Demo SXA Sites/NYSIF` (`{A10532BC-6FE9-4B72-82C1-4ED80599A86B}`).
5. **Save**.

| Content Editor label | Serialization name | Typical value |
|----------------------|--------------------|---------------|
| Compatible themes | `Themes` | NYSIF theme media item |
| EditingTheme | `EditingTheme` | Wireframe `{02D5A629-7196-483B-8FF0-00E61375F00F}` (CM preview only) |

> **Deploy shortcut:** `dotnet sitecore ser push` sets this automatically.

## Assign site theme (required for CD styling)

**Compatible themes** only makes NYSIF selectable. The **active** site theme is on the **Page Designs** folder:

| Open this item | `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Page Designs` |
|----------------|---------------------------------------------------------------------|
| Field | **Theme** (Styling section) |
| Value | Default device → `/sitecore/media library/Demo SXA Sites/NYSIF` |

Without this, CD falls back to **Wireframe** (gray / unstyled). Serialized as `Theme` on `Presentation/Page Designs.yml`.

In Content Editor: select **Page Designs** → **Content** tab → **Theme** → pick **NYSIF** → **Save** → publish.

## Tenant themes folder

| Item | Path |
|------|------|
| Demo SXA Sites tenant | `/sitecore/content/Demo SXA Sites` |
| ThemesFolder | `/sitecore/media library/Demo SXA Sites` |

The NYSIF theme item must exist under that Media Library folder before assignment.

## Base theme inheritance

NYSIF inherits base themes (Media Library):

- `/sitecore/media library/Base Themes/Core Libraries`
- `/sitecore/media library/Base Themes/Main Theme`
- `/sitecore/media library/Base Themes/Components Theme`
- `/sitecore/media library/Base Themes/Grid Theme`

View **BaseLayout** on the NYSIF theme item in Content Editor to confirm.

Theme development: [09-theme-dev-workflow.md](./09-theme-dev-workflow.md).
