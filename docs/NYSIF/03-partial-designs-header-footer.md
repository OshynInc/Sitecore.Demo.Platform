# 03 — Partial Designs: Header & Footer

Shared chrome for the NYSIF site.

> **Content Editor:** [00 — Content Editor basics](./00-content-editor-basics.md) · Layout Details on partial design items · **master** database.

## Deploy vs manual

Serialized header/footer deploy with `./up.ps1` or `dotnet sitecore ser push -n Project.NYSIF.Website`. Use the steps below to **recreate or edit** in Content Editor.

---

## NYSIF Header

**Partial design item:** `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Partial Designs/NYSIF Header`

**Content** tab:

| Field | Value |
|-------|-------|
| Signature | `nysif-header` |

**Layout Details:** root placeholder is **`header`** (type `header`, not `/header`).

### Header layout tree

```
header
├─ NYSIF Official Website Bar (uneditable)
└─ Container [id=1, Styles: row-dark]
   └─ Column Splitter [id=2]
      ├─ column-1-2 → Image (logo)
      └─ column-2-2 → Container [id=4]
         └─ Column Splitter [id=5]
            ├─ column-1-5 → NYSIF Navigation
            └─ column-2-5 → Container [id=7]
               ├─ Link (Login)
               └─ Link (Get a Quote)
```

### Header — Layout Details steps

| Step | Placeholder (type exactly) | Control | Edit → key parameters |
|------|----------------------------|---------|------------------------|
| H0 | `header` | NYSIF Official Website Bar | No datasource — hard-coded NY.gov chrome |
| H1 | `header` | Container | **DynamicPlaceholderId:** `1` · **Styles:** `row-dark` |
| H2 | `/header/container-1` | Column Splitter | **DynamicPlaceholderId:** `2` · set **Column 1** / **Column 2** grid per viewport (logo \| actions split; copy from another header or use equal widths on desktop) |
| H3 | `/header/container-1/column-1-2` | Image | **Datasource:** `/sitecore/content/Demo SXA Sites/NYSIF/Data/Header/Logo` |
| H4 | `/header/container-1/column-2-2` | Container | **DynamicPlaceholderId:** `4` |
| H5 | `/header/container-1/column-2-2/container-4` | Column Splitter | **DynamicPlaceholderId:** `5` · **Column 1** / **Column 2** for nav \| buttons |
| H6 | `/header/container-1/column-2-2/container-4/column-1-5` | Link List | **Datasource:** `/sitecore/content/Demo SXA Sites/NYSIF/Data/Navigation` · **Styles:** `nysif-header-nav` |
| H7 | `/header/container-1/column-2-2/container-4/column-2-5` | Container | **DynamicPlaceholderId:** `7` |
| H8 | `/header/container-1/column-2-2/container-4/column-2-5/container-7` | Link | **Datasource:** `.../Data/Header/Login` · **Variant:** `NYSIF Login Button` |
| H9 | same as H8 | Link | **Datasource:** `.../Data/Header/Get a Quote` · **Variant:** `NYSIF Quote Button` |

| Rendering | Path |
|-----------|------|
| Container | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Structure/Container` |
| Column Splitter | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Structure/Column Splitter` |
| Image | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Media/Image` |
| Link List | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Navigation/Link List` |
| Link | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Navigation/Link` |

> **Mega menu:** Replace Link List (H6) with **Navigation** rendering and follow [05-navigation-mega-menu.md](./05-navigation-mega-menu.md).

### Header presentation items

| Item | Path |
|------|------|
| Link List style | `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Styles/Navigation/nysif-header-nav` |
| Login link variant | `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Rendering Variants/Link/NYSIF Login Button` |
| Quote link variant | `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Rendering Variants/Link/NYSIF Quote Button` |
| row-dark | `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Styles/Layout/row-dark` |

---

## NYSIF Footer

**Partial design item:** `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Partial Designs/NYSIF Footer`

**Content** tab:

| Field | Value |
|-------|-------|
| Signature | `nysif-footer` |

**Layout Details:** root placeholder is **`footer`**.

### Footer layout tree (serialized)

```
footer
├─ Container [id=1, Styles: nysif-footer-dark, row-dark]
│  └─ Column Splitter [id=4] — four columns in serialization
│     ├─ column-1-4 → Image (logo)
│     ├─ column-2-4 → Link List (About)
│     ├─ column-3-4 → Link List (Resources)
│     └─ column-4-4 → Link List (Contact) + Link List (Social)
└─ Container [id=2, Styles: row-dark]
   └─ Container [id=3]
      └─ Page Content (copyright)
```

> **Note:** The serialized footer uses a **four-column** Column Splitter. The Content Editor Column Splitter UI exposes **two columns** only. For a manual build, either **deploy serialized content** or approximate with **nested two-column splitters**. Steps below match the serialized four-column layout (deploy recommended).

### Footer — Layout Details steps (serialized paths)

| Step | Placeholder | Control | Edit → key parameters |
|------|-------------|---------|------------------------|
| F1 | `footer` | Container | **DynamicPlaceholderId:** `1` · **Styles:** `nysif-footer-dark` + `row-dark` |
| F2 | `/footer/container-1` | Column Splitter | **DynamicPlaceholderId:** `4` · configure four column widths per viewport (deploy pre-sets this) |
| F3 | `/footer/container-1/column-1-4` | Image | **Datasource:** `.../Data/Header/Logo` |
| F4 | `/footer/container-1/column-2-4` | Link List | **Datasource:** `.../Data/Footer/About` |
| F5 | `/footer/container-1/column-3-4` | Link List | **Datasource:** `.../Data/Footer/Resources` |
| F6 | `/footer/container-1/column-4-4` | Link List | **Datasource:** `.../Data/Footer/Contact` |
| F7 | `/footer/container-1/column-4-4` | Link List | **Datasource:** `.../Data/Footer/Social Links` |
| F8 | `footer` | Container | **DynamicPlaceholderId:** `2` · **Styles:** `row-dark` |
| F9 | `/footer/container-2` | Container | **DynamicPlaceholderId:** `3` |
| F10 | `/footer/container-2/container-3` | Page Content | **Datasource:** `.../Data/Footer/Copyright` |

| Rendering | Path |
|-----------|------|
| Page Content | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Content/Page Content` |

| Style | Path |
|-------|------|
| nysif-footer-dark | `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Styles/Layout/nysif-footer-dark` |
| row-dark | `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Styles/Layout/row-dark` |

---

## Assign partial designs to page design

**Item:** `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Page Designs/NYSIF Homepage`

**Content** tab → **PartialDesigns** → select **NYSIF Header** and **NYSIF Footer** → **Save**.

| Partial design | Path |
|----------------|------|
| Header | `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Partial Designs/NYSIF Header` |
| Footer | `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Partial Designs/NYSIF Footer` |

## Source files

`Demo/src/items/Project.NYSIF.Website.Content/NYSIF/` — Partial Designs, Data, Presentation variants/styles.
