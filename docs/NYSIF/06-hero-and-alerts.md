# 06 — Hero & Service Alerts

Configure hero and service alerts datasources and bind them in Layout Details.

> **Content Editor:** [00 — Content Editor basics](./00-content-editor-basics.md) · Page layout: [04 — Page design](./04-page-design-homepage.md).

## Hero rendering item

| Lookup | Value |
|--------|-------|
| **Path** | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Hero` |
| **ID** | `{6BCF8D8E-0A28-4844-BFB4-96C852BAD2DC}` |
| **Tree** | layout → Renderings → Feature → Demo Shared → **SitecoreDemo XA Extensions** → Hero |

If missing: `dotnet sitecore ser push -i Feature.ExperienceAccelerator.Extensions` from `Demo/`.

> **Not here:** `/sitecore/layout/Renderings/Project/Demo Shared SXA Sites/SitecoreDemo`

---

## Hero datasource (Content Editor)

**Item:** `/sitecore/content/Demo SXA Sites/NYSIF/Data/Hero/Homepage Hero`

**Content** tab — example values:

| Field | Example |
|-------|---------|
| HeroSupertitle | `NEW YORK STATE INSURANCE FUND` |
| HeroTitle | `Protecting New York's Workforce Since 1914` |
| HeroText | Subtitle from static mock |
| HeroLink | Primary CTA (Get a Quote) |
| Stat1Value / Stat1Label | `1M+` / `Policyholders served` |
| Stat2Value / Stat2Label | `$2.5B` / `Claims paid annually` |
| Stat3Value / Stat3Label | `100+` / `Years of service` |
| Stat4Value / Stat4Label | `24/7` / `Claims support` |

| Template | Path |
|----------|------|
| Hero | `/sitecore/templates/Feature/Demo Shared/XA/Page Content/Hero` |
| Hero Folder | `/sitecore/templates/Feature/Demo Shared/XA/Page Content/Hero Folder` |

---

## Hero variant & style items

| Item | Path |
|------|------|
| Variant | `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Rendering Variants/Hero/NYSIF Homepage Hero` |
| Variant CssClass | `hero nysif-hero-homepage` |
| Style | `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Styles/Hero/nysif-hero-homepage` |

Variant field tree: eyebrow, H1, intro, CTAs, divider, 4-stat grid.

---

## Service Alerts datasource

**Item:** `/sitecore/content/Demo SXA Sites/NYSIF/Data/Service Alerts/Homepage Alerts`

| Field | Value |
|-------|-------|
| Template | `/sitecore/templates/Feature/Demo Shared/XA/Page Content/Service Alerts` |
| Badge | `Service Alerts` |

Add child alert items (Date + Text) under the folder.

---

## Service Alerts rendering & variant

| Item | Path |
|------|------|
| Rendering | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Service Alerts` |
| Variant | `/sitecore/content/Demo SXA Sites/NYSIF/Presentation/Rendering Variants/Service Alerts/Default` |

---

## Bind in Layout Details (page design)

On **NYSIF Homepage** page design ([04](./04-page-design-homepage.md)) — or on **Home** if overriding layout:

| Control | Placeholder (from doc 04) | Edit → parameters |
|---------|---------------------------|-------------------|
| Hero | `/main/container-1/column-1-2` | **Variant:** `NYSIF Homepage Hero` · **Styles:** `nysif-hero-homepage` · **Datasource:** `.../Data/Hero/Homepage Hero` |
| Service Alerts | `/main/container-1/column-2-2` | **Variant:** `Default` · **Datasource:** `.../Data/Service Alerts/Homepage Alerts` |

Steps:

1. Open Layout Details on the page design (or Home item).
2. Select the Hero row → **Edit** → set **Datasource**, **Variant**, **Styles** → **OK**.
3. Repeat for Service Alerts.
4. **Save** → publish.

Band wrapper (**Container** with **row-dark**) is configured in [04](./04-page-design-homepage.md) steps 1.1–1.2.

> Screenshot placeholder: Edit dialog on Hero control showing Datasource and Variant.
