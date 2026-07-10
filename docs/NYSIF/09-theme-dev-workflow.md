# 09 — Theme Development Workflow

Develop and deploy the NYSIF SXA theme from the repo. Verification uses **Content Editor** (Media Library).

> **Content Editor:** [00 — Content Editor basics](./00-content-editor-basics.md)

## Location

```
Demo/FrontEnd/NYSIF/
├── gulp/
│   ├── config.js
│   └── serverConfig.json    # themePath: \NYSIF
├── sass/
│   ├── main.scss
│   ├── abstracts/_vars-nysif.scss
│   ├── component-*.scss
│   ├── styles/
│   └── variants/
├── scripts/
│   └── component-navigation-nysif.js
└── package.json
```

## Setup

```bash
cd Demo/FrontEnd/NYSIF
npm install
```

## Upload / watch (development)

```bash
cd Demo/FrontEnd/NYSIF
npx gulp watch
```

Gulp uploads compiled CSS/JS to CM Media Library: `/sitecore/media library/Demo SXA Sites/NYSIF`.

## Deploy workflow (Lighthouse pattern)

`.\up.ps1` runs `build-themes.ps1 -Themes @("NYSIF")` before `dotnet sitecore ser push`:

1. Runs `npm run build` (or Dart Sass fallback)
2. Embeds blobs into `src/items/Project.Global.Website.Themes/Demo SXA Sites/NYSIF/`

Rebuild theme blobs only:

```bash
cd Demo
.\build-themes.ps1 -Themes @("NYSIF")
dotnet sitecore ser push -n Project.Global.Website
dotnet sitecore publish
```

## Verify in Content Editor

1. Jump to `/sitecore/media library/Demo SXA Sites/NYSIF/styles/` — confirm `component-*.css` items exist and **Blob** field has content after build.
2. Jump to `.../NYSIF/scripts/` — confirm `component-navigation-nysif.js`.
3. On `/sitecore/content/Demo SXA Sites/NYSIF/Settings` → **Compatible themes** includes the NYSIF theme ([02](./02-create-nysif-theme.md)).
4. Publish → browse CD with hard refresh (Ctrl+F5).

## Component CSS map

From `gulp/config.js` → `stylesConfig`:

| Rendering | Rendering path | SCSS file |
|-----------|----------------|-----------|
| navigation | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Navigation/Navigation` | component-navigation.scss |
| link-list | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Navigation/Link List` | component-link-list.scss |
| hero | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Hero` | component-hero.scss |
| service-alerts | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Service Alerts` | component-service-alerts.scss |
| role-cards | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Role Cards` | component-role-cards.scss |
| testimonial | `/sitecore/layout/Renderings/Feature/Demo Shared/XA/Testimonial` | component-testimonial.scss |
| promo | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Content/Promo` | component-promo.scss |
| page-list | `/sitecore/layout/Renderings/Feature/Experience Accelerator/Page Content/Page List` | component-page-list.scss |

## Styles vs variants

| Layer | Location | Purpose |
|-------|----------|---------|
| Author styles | `sass/styles/` + `.../Presentation/Styles/` | Pickable in Layout Details **Styles** parameter |
| Variants | `sass/variants/` | Variant-specific CSS |
| Components | `sass/component-*.scss` | Base rendering styles |

Static design reference: `Static/nysif_homepage.html`

Rendering paths: [10-serialization-deploy.md](./10-serialization-deploy.md#rendering-path-reference)
