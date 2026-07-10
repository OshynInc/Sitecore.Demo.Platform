# 04 — Page Design: NYSIF Homepage

Build the homepage in **Content Editor** / **Experience Editor** using **Layout Details** on the **Home** page item.

> **Prerequisites:** [00 — Content Editor basics](./00-content-editor-basics.md) (placeholders, Column Splitter, Layout Details workflow).

**Home page item (body Layout Details):** `/sitecore/content/Demo SXA Sites/NYSIF/Home`  
**Page design item:** `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Page Designs/NYSIF Homepage`

| Field | Value |
|-------|-------|
| Template | Page Design |
| PartialDesigns | NYSIF Header \| NYSIF Footer |

> **Demo note:** Homepage body bands live on the **Home** item (`__Renderings`) so authors can edit them directly in Experience Editor. Header and footer stay on Partial Designs.

**Partial design paths:**

| Partial design | Path |
|----------------|------|
| Header | `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Partial Designs/NYSIF Header` |
| Footer | `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Partial Designs/NYSIF Footer` |

> **Tip:** Paste any rendering path below into the Content Editor **tree jump box** (master database) to open the rendering definition item before adding it in Layout Details.

---

## Before you start

1. Open **Content Editor** on **master**.
2. On the **page design** item, set **PartialDesigns** to **NYSIF Header** and **NYSIF Footer**. Save.
3. Open **Layout Details** on the **Home** page item (not the page design / partials):
   - **Presentation** tab → **Details** (or **Layout Details**).
   - Confirm **Default** device is selected.
4. Or open **Home** in **Experience Editor** — body components are editable on the page.

When you **Add** a control, you type the placeholder into the **Placeholder** field (sometimes labeled **Add to placeholder**). It is a free-text field, not a dropdown.

---

## How placeholder paths work

SXA dynamic placeholders are built from two parts:

1. **DynamicPlaceholderId** — a number you set on each Container and Column Splitter in **Edit** → parameters.
2. **Path segment** — derived from that ID:
   - Container with `DynamicPlaceholderId=1` → inner placeholder **`container-1`**
   - Column Splitter with `DynamicPlaceholderId=2` → **`column-1-2`** (left) and **`column-2-2`** (right)

**Full path** = parent path + segment, with a leading `/` on nested paths (same pattern as the serialized NYSIF header):

| Level | Example placeholder to type |
|-------|----------------------------|
| Page design root | `main` |
| Inside container 1 | `/main/container-1` |
| Left column of splitter 2 | `/main/container-1/column-1-2` |
| Right column of splitter 2 | `/main/container-1/column-2-2` |

This guide uses **fixed DynamicPlaceholderId values** (1–7) so you can type each placeholder exactly as shown. When you **Edit** a parent control, set **DynamicPlaceholderId** to the value in the table before adding children.

If a path does not resolve, open the parent control’s **Edit** dialog and confirm its **DynamicPlaceholderId** matches the table — then adjust the typed path to use that number.

**Band background styles** (`row-dark`, `row-light`, `row-gray`) are set on **Container** controls in **Edit** → **Styles**. They are not part of the placeholder path.

Style items: `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Styles/Layout/`

### Column Splitter parameters (Content Editor)

The **Column Splitter** rendering always exposes **two columns** — **Column 1** and **Column 2**. There is no field to change the column count (no SplitterSize / EnabledPlaceholders in the UI).

When you **Edit** a Column Splitter, set:

| Field | What to set |
|-------|-------------|
| **DynamicPlaceholderId** | Fixed ID from the step table (`2` or `6`) — drives `column-1-{id}` / `column-2-{id}` placeholder paths |

Each column has **four grid width pickers**, one per SXA viewport:

| Viewport order (typical) | Breakpoint |
|--------------------------|------------|
| 1 | **Phones** |
| 2 | **Tablets** |
| 3 | **Desktops** |
| 4 | **Large Desktops** |

The picker labels come from your site’s **grid definition** (Bootstrap-style fractions such as 12/12, 8/12, 4/12 — exact text varies). You configure **Column 1** and **Column 2** separately for each viewport.

#### Recommended 8 / 4 layout (hero band and news band)

| Column | Phones | Tablets | Desktops | Large Desktops |
|--------|--------|---------|----------|----------------|
| **Column 1** (Hero, Page List) | Full width (12/12) | Full width (12/12) | Wide (8/12) | Wide (8/12) |
| **Column 2** (Service Alerts, Link Lists) | Full width (12/12) | Full width (12/12) | Narrow (4/12) | Narrow (4/12) |

On small viewports both columns stack at full width; on desktop they sit side-by-side 8+4.

#### Copy settings from an existing page (easiest)

To match a known-good 8/4 splitter without guessing grid labels:

1. In Content Editor, open **Layout Details** on `/sitecore/content/Demo SXA Sites/Cumulus/Home/search` (or any Lighthouse site **search** page).
2. **Edit** its Column Splitter and note **Column 1** / **Column 2** values for each viewport.
3. Apply the same picks on your NYSIF page design splitters (steps 1.2 and 4.2).

> **Note:** `SplitterSize` and `EnabledPlaceholders` appear in serialized layout XML only — they are not fields in the Content Editor Edit dialog.

---

## Target layout

```
main
├─ Container [id=1, row-dark]
│  └─ Column Splitter [id=2, 8/4]
│     ├─ Hero
│     └─ Service Alerts
├─ Container [id=3, row-light]
│  └─ Role Cards
├─ Container [id=4, row-gray]
│  └─ Promo ×4
├─ Container [id=5]
│  └─ Column Splitter [id=6, 8/4]
│     ├─ Page List
│     └─ Link List ×2
└─ Container [id=7, row-gray]
   └─ Testimonial
```

`[id=n]` = **DynamicPlaceholderId** to set on that control.

---

## Step-by-step: Layout Details

For each row: **Add** → type **Placeholder** exactly → select **Control** → **Edit** parameters → **OK** → Save the page design.

### Band 1 — Hero + Service Alerts

| Step | Placeholder (type exactly) | Control (rendering path) | Edit → parameters |
|------|----------------------------|--------------------------|-------------------|
| 1.1 | `main` | Container — `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Structure/Container` | **DynamicPlaceholderId:** `1` · **Styles:** `row-dark` |
| 1.2 | `/main/container-1` | Column Splitter — `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Structure/Column Splitter` | **DynamicPlaceholderId:** `2` · **Column 1 / Column 2:** 8/4 grid per viewport (see table above; copy from Cumulus search page) |
| 1.3 | `/main/container-1/column-1-2` | Hero — `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Hero` | **Variant:** `NYSIF Homepage Hero` |
| 1.4 | `/main/container-1/column-2-2` | Service Alerts — `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Service Alerts` | **Variant:** `Default` |

### Band 2 — Role Cards

| Step | Placeholder (type exactly) | Control (rendering path) | Edit → parameters |
|------|----------------------------|--------------------------|-------------------|
| 2.1 | `main` | Container | **DynamicPlaceholderId:** `3` · **Styles:** `row-light` |
| 2.2 | `/main/container-3` | Role Cards — `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Role Cards` | **Variant:** `Default` |

### Band 3 — Quick actions (Promos)

| Step | Placeholder (type exactly) | Control (rendering path) | Edit → parameters |
|------|----------------------------|--------------------------|-------------------|
| 3.1 | `main` | Container | **DynamicPlaceholderId:** `4` · **Styles:** `row-gray` |
| 3.2 | `/main/container-4` | Promo — `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Content/Promo` | Add **four times**. **Variant:** `NYSIF Quick Action` · **Styles:** `nysif-quick-action` (see [07](./07-content-sections.md)) |

### Band 4 — News + sidebar

| Step | Placeholder (type exactly) | Control (rendering path) | Edit → parameters |
|------|----------------------------|--------------------------|-------------------|
| 4.1 | `main` | Container | **DynamicPlaceholderId:** `5` · *(leave Styles empty)* |
| 4.2 | `/main/container-5` | Column Splitter | **DynamicPlaceholderId:** `6` · **Column 1 / Column 2:** same 8/4 viewport grid as step 1.2 |
| 4.3 | `/main/container-5/column-1-6` | Page List — `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Content/Page List` | **Variant:** `NYSIF Featured` or `NYSIF Row` |
| 4.4 | `/main/container-5/column-2-6` | Link List — `/sitecore/layout/Renderings/Feature/Experience Accelerator/Navigation/Link List` | Add **twice**. **Styles:** `nysif-notice-list`, `nysif-sidebar-links` |

### Band 5 — Testimonials

| Step | Placeholder (type exactly) | Control (rendering path) | Edit → parameters |
|------|----------------------------|--------------------------|-------------------|
| 5.1 | `main` | Container | **DynamicPlaceholderId:** `7` · **Styles:** `row-gray` |
| 5.2 | `/main/container-7` | Testimonial — `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Testimonial` | **Variant:** `Default` |

---

## Placeholder path reference

| DynamicPlaceholderId | Control | Placeholder path to type for children |
|----------------------|---------|---------------------------------------|
| 1 | Container (hero) | `/main/container-1` |
| 2 | Column Splitter (hero) | `/main/container-1/column-1-2` · `/main/container-1/column-2-2` |
| 3 | Container (roles) | `/main/container-3` |
| 4 | Container (quick actions) | `/main/container-4` |
| 5 | Container (news) | `/main/container-5` |
| 6 | Column Splitter (news) | `/main/container-5/column-1-6` · `/main/container-5/column-2-6` |
| 7 | Container (testimonials) | `/main/container-7` |

---

## Layout Details reference (all renderings)

| Control | Rendering path |
|---------|----------------|
| Container | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Structure/Container` |
| Column Splitter | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Structure/Column Splitter` |
| Hero | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Hero` |
| Service Alerts | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Service Alerts` |
| Role Cards | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Role Cards` |
| Promo | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Content/Promo` |
| Page List | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Content/Page List` |
| Link List | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Navigation/Link List` |
| Testimonial | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Testimonial` |

---

## Row styles (Container only)

Path: `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Styles/Layout/`

| Style | Path | CSS class |
|-------|------|-----------|
| row-dark | `.../Layout/row-dark` | `row-dark` |
| row-light | `.../Layout/row-light` | `row-light` |
| row-gray | `.../Layout/row-gray` | `row-gray` |

In Layout Details: select the Container row → **Edit** → **Styles** → pick the style item.

---

## Assign page design to Home

1. Navigate to `/sitecore/content/Demo SXA Sites/NYSIF/Home`.
2. On the **Content** tab, set **Page Design** to **NYSIF Homepage**.
3. Save.

Datasource binding and publish steps: [08-compose-homepage.md](./08-compose-homepage.md).

---

## Checklist

- [ ] PartialDesigns = NYSIF Header + NYSIF Footer on the page design item
- [ ] Body bands live on the **Home** item Layout Details (editable in Experience Editor)
- [ ] Each Container / Column Splitter has the **DynamicPlaceholderId** from the step table
- [ ] Placeholder paths typed exactly (`main`, `/main/container-1`, …)
- [ ] Five Containers directly under `main`, in order
- [ ] Hero, Service Alerts, Page List, and Link Lists in column placeholders (not `main`)
- [ ] Band styles on Containers only
- [ ] Home **Page Design** field = NYSIF Homepage
- [ ] Saved and published when ready

> Screenshot placeholder: Layout Details **Add** dialog with Placeholder field and DynamicPlaceholderId on a Container.
