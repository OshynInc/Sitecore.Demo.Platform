# 05 — Navigation Mega Menu

Configure navigation data and (optionally) upgrade the header to the full Navigation rendering.

> **Content Editor:** [00 — Content Editor basics](./00-content-editor-basics.md) · [03 — Header partial design](./03-partial-designs-header-footer.md) for Layout Details steps.

## Navigation datasource (Content Editor)

**Folder item:** `/sitecore/content/Demo SXA Sites/NYSIF/Data/Navigation`

Create or edit link items under this folder on the **Content** tab:

| Nav label | Notes |
|-----------|-------|
| For Employers | Add child **Link** items for tertiary panels (Review My Account, etc.) |
| Injured Workers | Top-level link |
| Brokers & Agents | Top-level link |
| Medical Providers | Top-level link |
| About NYSIF | Top-level link |

| Item type | Template path |
|-----------|---------------|
| Navigation root (folder) | `/sitecore/templates/Feature/Experience Accelerator/Navigation/Link List` |
| Nav link | `/sitecore/templates/Feature/Experience Accelerator/Datasource/Link` |

For **Employers** mega panels, add child links; theme JS (`component-navigation-nysif.js`) uses `data-sub` values (`wcp`, `dbp`, `lfi`, `safety`) for panel swapping.

### Insert a nav link manually

1. Select `Data/Navigation` (or a parent nav item).
2. **Insert** → **Link** (or compatible template).
3. Fill **Link** field, **Text**, etc. on **Content** tab.
4. **Save** → publish.

---

## Rendering definitions

| Purpose | Rendering path |
|---------|----------------|
| Top-level links (serialized demo) | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Navigation/Link List` |
| Full mega menu | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Navigation/Navigation` |

Jump to rendering paths in the tree jump box to verify they exist.

---

## Rendering variant (Navigation mega menu)

**Item:** `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Rendering Variants/Navigation/NYSIF Mega Menu`

Edit on **Content** tab / variant fields:

| Field | Value |
|-------|-------|
| CssClass | `navigation nysif-mega-nav` |
| Compatible Renderings | Navigation `{9D3BA81A-EECE-4B5A-9862-0239A9D53EEC}` |

Build the variant field tree for mega-menu markup (tabs, columns, panels). Use a **Scriban** variant if the field tree is too deep.

---

## Styles (Content Editor)

| Style | Path | Value | Allowed renderings |
|-------|------|-------|-------------------|
| nysif-mega-nav | `.../Presentation/Styles/Navigation/nysif-mega-nav` | `nysif-mega-nav` | Navigation |
| nysif-header-nav | `.../Presentation/Styles/Navigation/nysif-header-nav` | `navigation nysif-mega-nav` | Link List |

Open each style item to view **Value** and **Allowed Renderings** on the **Content** tab.

---

## Header component (Layout Details)

**Partial design:** `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Partial Designs/NYSIF Header`

**Presentation** tab → **Layout Details** → find the nav control row → **Edit**.

### Option A — Link List (serialized demo)

| Parameter | Value |
|-----------|-------|
| Control | Link List |
| Datasource | `/sitecore/content/Demo SXA Sites/NYSIF/Data/Navigation` |
| Styles | `nysif-header-nav` |

### Option B — Navigation mega menu

Replace the Link List control (or change rendering):

| Parameter | Value |
|-----------|-------|
| Control | Navigation |
| Variant | `NYSIF Mega Menu` |
| Styles | `nysif-mega-nav` |
| NavigationRoot / Datasource | `/sitecore/content/Demo SXA Sites/NYSIF/Data/Navigation` |

**Save** the partial design → publish.

---

## Mobile menu

Mega menu variant should include mobile markup (`hamburgerBtn`, `mobileMenuPanel`, `mobileAccordionRoot`, `mobileDrilldown`). Theme JS handles accordion and drill-down.

> Screenshot placeholder: Layout Details → Edit on Link List showing Datasource and Styles.
