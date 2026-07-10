# 00 — Content Editor Basics (NYSIF)

Shared conventions for all NYSIF guides. Use **Content Editor** on the **master** database unless noted otherwise.

## Navigation

| Action | How |
|--------|-----|
| Open an item by path | Paste the full path into the **tree jump box** at the top of the content tree |
| Open a rendering definition | Jump to `/sitecore/layout/Renderings/...` or click a control in **Layout Details** → **Edit** |
| Show all fields | **View → Show Standard Fields** (needed for some Site Settings fields) |

## Layout Details workflow

Used for partial designs, page designs, and pages.

1. Select the item (partial design, page design, or page).
2. **Presentation** tab → **Details** (or **Layout Details**).
3. Confirm **Default** device is selected.
4. **Add** a control:
   - **Placeholder** — type the placeholder path (free-text field; not a dropdown).
   - **Control** — pick the rendering from the tree, or jump to the rendering item first and select it.
5. Select the new row → **Edit** → set **DynamicPlaceholderId**, **Styles**, **Variant**, **Datasource**, etc.
6. **OK** → **Save** the item.

Repeat **Save** after adding parent controls so nested placeholder paths are valid before adding children.

## Placeholder paths

SXA builds paths from **DynamicPlaceholderId** on Container and Column Splitter controls.

| Parent | DynamicPlaceholderId | Child placeholder segment |
|--------|----------------------|---------------------------|
| Container | `n` | `container-{n}` |
| Column Splitter | `n` | `column-1-{n}` (Column 1), `column-2-{n}` (Column 2) |

| Nesting level | Example placeholder to type |
|---------------|----------------------------|
| Root (`main`, `header`, `footer`) | `main` *(no leading slash)* |
| First nested level | `/main/container-1` |
| Deeper nesting | `/main/container-1/column-1-2` |

Set **DynamicPlaceholderId** on the parent in **Edit** before adding children. This guide series uses fixed IDs in step tables so paths can be typed exactly.

## Column Splitter

- Always **two columns**: **Column 1** and **Column 2**. There is no Content Editor field to change column count.
- Each column has **four grid width pickers** — one per viewport: **Phones**, **Tablets**, **Desktops**, **Large Desktops**.
- Labels come from the site grid definition (e.g. 12/12, 8/12, 4/12).
- `SplitterSize` and `EnabledPlaceholders` exist in serialized XML only — not in the Edit dialog.

**8 / 4 side-by-side (desktop), stacked (mobile):**

| Column | Phones | Tablets | Desktops | Large Desktops |
|--------|--------|---------|----------|----------------|
| Column 1 | Full width (12/12) | Full width | Wide (8/12) | Wide (8/12) |
| Column 2 | Full width (12/12) | Full width | Narrow (4/12) | Narrow (4/12) |

Copy grid picks from an existing demo page: Layout Details on `/sitecore/content/Demo SXA Sites/Cumulus/Home/search` → **Edit** its Column Splitter.

## Rendering parameters (common)

Set in Layout Details → select control → **Edit**:

| Parameter | Used for |
|-----------|----------|
| **DynamicPlaceholderId** | Placeholder path segments (`container-1`, `column-1-2`, …) |
| **Styles** | SXA style items under `.../Presentation/Styles/` (e.g. row-dark, nysif-header-nav) |
| **Variant** / **FieldNames** | Rendering variant under `.../Presentation/Rendering Variants/` |
| **Datasource** / **Data Source** | Content item under `.../NYSIF/Data/` |
| **GridParameters** | Optional grid wrapper on some renderings (Container, Image, etc.) |

Band styles (`row-dark`, `row-light`, `row-gray`) apply to **Container** controls only — path: `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Styles/Layout/`.

## Page design assignment

On a page item (e.g. Home): **Content** tab → **Page Design** field → select **NYSIF Homepage**. Save.

Not the Experience Editor Design tab — use the **Content** tab field in Content Editor.

## Partial design assignment

On the page design item: **Content** tab → **PartialDesigns** → pick header and footer partial designs. Save.

## Publish

After content or layout changes:

1. Select the item (or site root) in Content Editor.
2. **Publish** tab → publish item and subitems (master → web).
3. Or run `dotnet sitecore publish` from `Demo/` (see [10-serialization-deploy.md](./10-serialization-deploy.md)).

## Custom renderings (Hero, etc.)

| Lookup | Hero example |
|--------|--------------|
| Path | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Hero` |
| ID | `{6BCF8D8E-0A28-4844-BFB4-96C852BAD2DC}` |
| Tree folder display name | **SitecoreDemo XA Extensions** (under Feature → Demo Shared) |

If missing: `dotnet sitecore ser push -i Feature.ExperienceAccelerator.Extensions` from `Demo/`.

Full rendering list: [10-serialization-deploy.md](./10-serialization-deploy.md#rendering-path-reference).

## Deploy vs manual build

Many NYSIF items are **serialized**. For a fresh environment, run `./up.ps1` instead of rebuilding everything by hand. Manual guides describe how to recreate or adjust items in Content Editor when needed.
