# 07 — Content Sections

Configure datasources and Layout Details bindings for homepage body sections.

> **Content Editor:** [00 — Content Editor basics](./00-content-editor-basics.md) · Page layout: [04 — Page design](./04-page-design-homepage.md).

---

## Role Cards

### Datasource

**Item:** `/sitecore/content/Demo SXA Sites/NYSIF/Data/Role Cards/Homepage Roles`

**Quick check:** On the **Content** tab, the template name (Quick Info / item header) must be **Role Cards**, not **Service Alerts**. Fields must be **Eyebrow**, **Title**, **Subtitle** — not Badge / Date / Text. If you see Service Alerts fields, the item has the wrong template; re-push serialization (below) or recreate the item from the Role Cards template.

**Content** tab (parent **Role Cards** item):

| Field | Example |
|-------|---------|
| Eyebrow | `WHO WE SERVE` |
| Title | `Insurance solutions for every role` |
| Subtitle | `Find resources tailored to how you work with NYSIF.` |

Add 6 child **Role Card** items (Icon, Title, Description): Employer, Injured Worker, Broker, Medical Provider, Attorney, HR Payroll.

| Template | Path |
|----------|------|
| Role Cards Folder | `/sitecore/templates/Feature/Demo Shared/XA/Page Content/Role Cards Folder` |
| Role Cards (parent) | `/sitecore/templates/Feature/Demo Shared/XA/Page Content/Role Cards` |
| Role Card (child) | `/sitecore/templates/Feature/Demo Shared/XA/Page Content/Role Card` |

If CM still shows the wrong template after a prior deploy:

```bash
cd Demo
dotnet sitecore ser push -n Project.NYSIF.Website
dotnet sitecore publish
```

### Layout Details (page design step 2.x)

| Placeholder | Control | Edit → parameters |
|-------------|---------|-------------------|
| `main` | Container | **DynamicPlaceholderId:** `3` · **Styles:** `row-light` |
| `/main/container-3` | Role Cards | **Variant:** `Default` · **Datasource:** `.../Data/Role Cards/Homepage Roles` |

| Rendering | Path |
|-----------|------|
| Role Cards | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Role Cards` |
| Variant | `.../Rendering Variants/Role Cards/Default` |

---

## Quick Actions (Promo ×4)

### Datasources

**Folder:** `/sitecore/content/Demo SXA Sites/NYSIF/Data/Quick Actions/`

Create four **Promo** items: File a Claim, Pay Your Bill, Find a Provider, Safety Resources.

| Template | Path |
|----------|------|
| Promo | `/sitecore/templates/Feature/Experience Accelerator/Page Content/Promo` |

### Layout Details (page design step 3.x)

| Placeholder | Control | Edit → parameters |
|-------------|---------|-------------------|
| `main` | Container | **DynamicPlaceholderId:** `4` · **Styles:** `row-gray` |
| `/main/container-4` | Promo *(×4)* | **Variant:** `NYSIF Quick Action` · **Styles:** `nysif-quick-action` · **Datasource:** each Quick Action item |

| Rendering | Path |
|-----------|------|
| Promo | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Content/Promo` |
| Variant | `.../Rendering Variants/Promo/NYSIF Quick Action` |
| Style | `.../Presentation/Styles/Promo/nysif-quick-action` |

Add four separate Promo controls to the same placeholder `/main/container-4`, each with its own datasource.

---

## News (Page List)

### Datasources

**Folder:** `/sitecore/content/Demo SXA Sites/NYSIF/Data/News/` — create sample news pages.

### Layout Details (page design step 4.x)

| Placeholder | Control | Edit → parameters |
|-------------|---------|-------------------|
| `/main/container-5/column-1-6` | Page List | **Variant:** `NYSIF Featured` or `NYSIF Row` · **Source:** `.../Data/News` |

| Rendering | Path |
|-----------|------|
| Page List | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Content/Page List` |
| Variants | `.../Rendering Variants/Page List/NYSIF Featured` · `NYSIF Row` |

Column Splitter and news-band Container: [04](./04-page-design-homepage.md) steps 4.1–4.2.

---

## Sidebar Link Lists

### Datasources

Create Link List folders under `Data/` (or use existing footer/notice folders) with child link items.

### Layout Details (page design step 4.4)

Add **two** Link List controls to **`/main/container-5/column-2-6`**:

| Control | Styles | Content |
|---------|--------|---------|
| Link List | `nysif-notice-list` | 3–4 notice links |
| Link List | `nysif-sidebar-links` | Safety resource links |

| Rendering | Path |
|-----------|------|
| Link List | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Navigation/Link List` |
| Styles | `.../Styles/Link List/nysif-notice-list` · `nysif-sidebar-links` |
| Datasource template | `/sitecore/templates/Feature/Experience Accelerator/Navigation/Link List` |

---

## Testimonials

### Datasource

**Item:** `/sitecore/content/Demo SXA Sites/NYSIF/Data/Testimonials/Homepage Testimonials`

**Quick check:** Template must be **Testimonial** (fields **Eyebrow**, **Title**), not Service Alerts. Children must be **Testimonial Card** (Quote, Name, Role, Initials).

**Content** tab (parent):

| Field | Example |
|-------|---------|
| Eyebrow | `WHAT POLICYHOLDERS SAY` |
| Title | `Trusted by New York businesses` |

Add 3 child **Testimonial Card** items (Quote, Name, Role, Initials).

| Template | Path |
|----------|------|
| Testimonials Folder | `/sitecore/templates/Feature/Demo Shared/XA/Page Content/Testimonials Folder` |
| Testimonial (parent) | `/sitecore/templates/Feature/Demo Shared/XA/Page Content/Testimonial` |
| Testimonial Card (child) | `/sitecore/templates/Feature/Demo Shared/XA/Page Content/Testimonial Card` |

### Layout Details (page design step 5.x)

| Placeholder | Control | Edit → parameters |
|-------------|---------|-------------------|
| `main` | Container | **DynamicPlaceholderId:** `7` · **Styles:** `row-gray` |
| `/main/container-7` | Testimonial | **Variant:** `Default` · **Datasource:** `.../Data/Testimonials/Homepage Testimonials` |

| Rendering | Path |
|-----------|------|
| Testimonial | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Testimonial` |
| Variant | `.../Rendering Variants/Testimonial/Default` |

---

## After editing

**Save** page design (and Home item if datasources set there) → **Publish** (see [08](./08-compose-homepage.md)).
