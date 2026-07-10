# 08 — Compose Homepage & Publish

Finalize the NYSIF homepage in **Content Editor**: assign page design, verify layout, bind datasources, publish.

> **Content Editor:** [00 — Content Editor basics](./00-content-editor-basics.md)

## Prerequisites

- [01](./01-create-nysif-site.md) through [07](./07-content-sections.md)
- Page design built per [04](./04-page-design-homepage.md)
- Theme assigned per [02](./02-create-nysif-theme.md)
- Datasource items populated under `/sitecore/content/Demo SXA Sites/NYSIF/Data/`

---

## 1. Assign page design to Home

**Item:** `/sitecore/content/Demo SXA Sites/NYSIF/Home`

**Content** tab:

| Field | Value |
|-------|-------|
| Page Design | **NYSIF Homepage** (`/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Page Designs/NYSIF Homepage`) |

**Save**.

---

## 2. Verify layout (Layout Details)

Select **Home** → **Presentation** tab → **Layout Details**.

Confirm body structure matches [04](./04-page-design-homepage.md). Homepage body bands are on **Home** (`__Renderings`) so they edit in Experience Editor. Header/footer come from the page design’s Partial Designs.

If body components are missing, edit **Home** Layout Details (not the page design).

---

## 3. Bind datasources & parameters

For each content control, select the row → **Edit**:

| Component | Variant | Style | Datasource |
|-----------|---------|-------|------------|
| Hero | `Rendering Variants/Hero/NYSIF Homepage Hero` | `Styles/Hero/nysif-hero-homepage` | `Data/Hero/Homepage Hero` |
| Service Alerts | `Rendering Variants/Service Alerts/Default` | — | `Data/Service Alerts/Homepage Alerts` |
| Role Cards | `Rendering Variants/Role Cards/Default` | — | `Data/Role Cards/Homepage Roles` |
| Quick Actions | NYSIF Quick Actions controller | — | `Data/Quick Actions` |
| Page List | NYSIF Page List / featured markup | — | `Data/News` |
| News Sidebar | NYSIF News Sidebar | — | `Data/News Sidebar` (or Notices + Safety folders) |
| Testimonial | NYSIF Testimonial | — | `Data/Testimonials/Homepage Testimonials` |

Paths relative to `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/` and `.../Data/`.

Header/footer come from partial designs ([03](./03-partial-designs-header-footer.md)) — verify nav datasource on **NYSIF Header** partial if links are empty.

**Save** Home and page design items.

---

## 4. Publish

In Content Editor:

1. Select `/sitecore/content/Demo SXA Sites/NYSIF` (or Home).
2. **Publish** tab → publish item and subitems (**master** → **web**).

Or from `Demo/`:

```bash
dotnet sitecore publish
```

---

## 5. CD verification

Browse: **https://cd.lighthouse.localhost/nysif**

| Check | Expected |
|-------|----------|
| Theme | NYSIF CSS/JS (not Wireframe only) |
| Header / footer | Partial designs render |
| Hero band | Dark row, hero + service alerts |
| Body sections | Role cards, promos, news, testimonials |

Hard refresh (Ctrl+F5) if styles are cached.

---

## Responsive QA

Compare to `Static/nysif_homepage.html`:

| Breakpoint | Width |
|------------|-------|
| Desktop nav collapse | 1130px |
| Tablet | 900px |
| Mobile nav | 760px |
| Small mobile | 480px |

Verify: nav, hero stack on mobile, role grid, quick actions grid, news layout.

> Screenshot placeholder: Home item Content tab showing Page Design field.
