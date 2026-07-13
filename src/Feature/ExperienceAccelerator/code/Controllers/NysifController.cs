using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Sitecore.Resources.Media;
using Sitecore.XA.Foundation.Mvc.Controllers;
using Sitecore.Demo.Platform.Feature.ExperienceAccelerator.Models;

namespace Sitecore.Demo.Platform.Feature.ExperienceAccelerator.Controllers
{
	public class NysifController : StandardController
	{
		public ActionResult Hero()
		{
			return View("~/Views/Nysif/Hero.cshtml", BuildHeroModel());
		}

		public ActionResult ServiceAlerts()
		{
			return View("~/Views/Nysif/ServiceAlerts.cshtml", BuildServiceAlertsModel());
		}

		public ActionResult RoleCards()
		{
			return View("~/Views/Nysif/RoleCards.cshtml", BuildRoleCardsModel());
		}

		public ActionResult Testimonial()
		{
			return View("~/Views/Nysif/Testimonial.cshtml", BuildTestimonialModel());
		}

		public ActionResult Promo()
		{
			return View("~/Views/Nysif/Promo.cshtml", BuildPromoModel());
		}

		public ActionResult QuickActions()
		{
			return View("~/Views/Nysif/QuickActions.cshtml", BuildQuickActionsModel());
		}

		public ActionResult PageList()
		{
			return View("~/Views/Nysif/PageList.cshtml", BuildPageListModel());
		}

		public ActionResult Navigation()
		{
			return View("~/Views/Nysif/Navigation.cshtml", BuildNavigationModel());
		}

		public ActionResult NewsSidebar()
		{
			return View("~/Views/Nysif/NewsSidebar.cshtml", BuildNewsSidebarModel());
		}

		private Item GetDatasource()
		{
			return RenderingContext.Current?.Rendering?.Item;
		}

		private static bool IsExperienceEditor => Sitecore.Context.PageMode.IsExperienceEditor;

		private static string Field(Item item, string name)
		{
			return item == null ? string.Empty : item[name] ?? string.Empty;
		}

		private static string LinkUrl(Item item, string fieldName)
		{
			if (item == null)
			{
				return "#";
			}

			LinkField link = item.Fields[fieldName];
			if (link == null || string.IsNullOrWhiteSpace(link.Value))
			{
				return "#";
			}

			if (link.IsInternal && link.TargetItem != null)
			{
				return LinkManager.GetItemUrl(link.TargetItem);
			}

			if (link.IsMediaLink && link.TargetItem != null)
			{
				return MediaManager.GetMediaUrl(link.TargetItem);
			}

			return string.IsNullOrWhiteSpace(link.Url) ? "#" : link.Url;
		}

		private static string LinkText(Item item, string fieldName, string fallback)
		{
			if (item == null)
			{
				return fallback;
			}

			LinkField link = item.Fields[fieldName];
			if (link == null || string.IsNullOrWhiteSpace(link.Value) || string.IsNullOrWhiteSpace(link.Text))
			{
				return fallback;
			}

			return link.Text;
		}

		private static string IconGlyph(string key)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				return "•";
			}

			switch (key.Trim().ToLowerInvariant())
			{
				case "briefcase":
					return "💼";
				case "bandage":
				case "injured":
					return "🩹";
				case "tie":
				case "broker":
					return "👔";
				case "medical":
				case "stethoscope":
					return "🩺";
				case "scales":
				case "attorney":
					return "⚖️";
				case "document":
				case "hr":
				case "clipboard":
					return "📋";
				case "calendar":
					return "📅";
				case "pin":
				case "location":
					return "📍";
				case "quote":
				case "file":
					return "📄";
				case "grad":
				case "training":
					return "🎓";
				default:
					return key.Length <= 2 ? key : "📄";
			}
		}

		private HeroModel BuildHeroModel()
		{
			var item = GetDatasource();
			var model = new HeroModel
			{
				Item = item,
				Supertitle = Field(item, "HeroSupertitle"),
				TitleHtml = Field(item, "HeroTitle"),
				Text = Field(item, "HeroText"),
				PrimaryCtaUrl = LinkUrl(item, "HeroLink"),
				PrimaryCtaText = LinkText(item, "HeroLink", "Get a Quote"),
				SecondaryCtaUrl = "#",
				SecondaryCtaText = "File a Claim",
				Stats = new List<HeroStat>()
			};

			if (item != null)
			{
				for (var i = 1; i <= 4; i++)
				{
					var value = Field(item, "Stat" + i + "Value");
					var label = Field(item, "Stat" + i + "Label");
					if (IsExperienceEditor || !string.IsNullOrWhiteSpace(value) || !string.IsNullOrWhiteSpace(label))
					{
						model.Stats.Add(new HeroStat { Value = value, Label = label });
					}
				}
			}

			return model;
		}

		private ServiceAlertsModel BuildServiceAlertsModel()
		{
			var item = GetDatasource();
			var model = new ServiceAlertsModel
			{
				Item = item,
				Badge = Field(item, "Badge"),
				Alerts = new List<ServiceAlertItem>()
			};

			if (item == null)
			{
				return model;
			}

			if (string.IsNullOrWhiteSpace(model.Badge))
			{
				model.Badge = "Service Alerts";
			}

			foreach (Item child in item.Children)
			{
				var date = Field(child, "Date");
				var text = Field(child, "Text");
				if (!IsExperienceEditor && string.IsNullOrWhiteSpace(date) && string.IsNullOrWhiteSpace(text))
				{
					continue;
				}

				model.Alerts.Add(new ServiceAlertItem
				{
					Item = child,
					Date = date,
					Text = text
				});
			}

			return model;
		}

		private RoleCardsModel BuildRoleCardsModel()
		{
			var item = GetDatasource();
			var model = new RoleCardsModel
			{
				Item = item,
				Eyebrow = Field(item, "Eyebrow"),
				Title = Field(item, "Title"),
				Subtitle = Field(item, "Subtitle"),
				Cards = new List<RoleCardItem>()
			};

			if (item == null)
			{
				return model;
			}

			foreach (Item child in item.Children)
			{
				model.Cards.Add(new RoleCardItem
				{
					Item = child,
					Icon = IconGlyph(Field(child, "Icon")),
					Title = Field(child, "Title"),
					Description = Field(child, "Description")
				});
			}

			return model;
		}

		private TestimonialModel BuildTestimonialModel()
		{
			var item = GetDatasource();
			var model = new TestimonialModel
			{
				Item = item,
				Eyebrow = Field(item, "Eyebrow"),
				Title = Field(item, "Title"),
				Cards = new List<TestimonialCardItem>()
			};

			if (item == null)
			{
				return model;
			}

			foreach (Item child in item.Children)
			{
				model.Cards.Add(new TestimonialCardItem
				{
					Item = child,
					Quote = Field(child, "Quote"),
					Name = Field(child, "Name"),
					Role = Field(child, "Role"),
					Initials = Field(child, "Initials")
				});
			}

			return model;
		}

		private PromoModel BuildPromoModel()
		{
			return BuildPromoFromItem(GetDatasource());
		}

		private PromoModel BuildPromoFromItem(Item item)
		{
			return new PromoModel
			{
				Item = item,
				Icon = IconGlyph(Field(item, "PromoIcon")),
				Title = Field(item, "PromoText"),
				Description = Field(item, "PromoText2"),
				LinkUrl = LinkUrl(item, "PromoLink"),
				LinkText = LinkText(item, "PromoLink", "Learn more")
			};
		}

		private QuickActionsModel BuildQuickActionsModel()
		{
			var item = GetDatasource();
			var model = new QuickActionsModel
			{
				Item = item,
				Heading = "Quick actions",
				Subheading = "The most common tasks — available right here, no login required to start.",
				Cards = new List<PromoModel>()
			};

			if (item == null)
			{
				return model;
			}

			foreach (Item child in item.Children)
			{
				model.Cards.Add(BuildPromoFromItem(child));
			}

			return model;
		}

		private PageListModel BuildPageListModel()
		{
			var item = GetDatasource();
			var model = new PageListModel
			{
				Item = item,
				Heading = "News & public affairs",
				Subheading = "Policy updates, industry insights, and NYSIF announcements.",
				Items = new List<NewsItemModel>()
			};

			if (item == null)
			{
				return model;
			}

			foreach (Item child in item.Children.Take(4))
			{
				model.Items.Add(new NewsItemModel
				{
					Item = child,
					Title = string.IsNullOrWhiteSpace(Field(child, "Title")) ? child.DisplayName : Field(child, "Title"),
					Summary = Field(child, "Summary"),
					Category = Field(child, "Category"),
					PublishDate = Field(child, "PublishDate"),
					Url = LinkManager.GetItemUrl(child)
				});
			}

			return model;
		}

		private NewsSidebarModel BuildNewsSidebarModel()
		{
			var item = GetDatasource();
			var model = new NewsSidebarModel
			{
				Item = item,
				NoticesHeading = "Important notices",
				Notices = new List<NoticeItemModel>(),
				SafetyHeading = "Workplace safety resources",
				SafetyItems = new List<SafetyItemModel>()
			};

			if (item == null)
			{
				return model;
			}

			var noticesFolder = item.Children["Important Notices"] ?? item.Axes.GetChild("Important Notices");
			var safetyFolder = item.Children["Safety Resources"] ?? item.Axes.GetChild("Safety Resources");
			model.NoticesFolder = noticesFolder;
			model.SafetyFolder = safetyFolder;

			if (noticesFolder != null)
			{
				if (!string.IsNullOrWhiteSpace(Field(noticesFolder, "Title")))
				{
					model.NoticesHeading = Field(noticesFolder, "Title");
				}

				foreach (Item notice in noticesFolder.Children)
				{
					var text = Field(notice, "Text");
					var date = Field(notice, "Date");
					if (!IsExperienceEditor && string.IsNullOrWhiteSpace(text) && string.IsNullOrWhiteSpace(date))
					{
						continue;
					}

					if (!IsExperienceEditor && string.IsNullOrWhiteSpace(text))
					{
						text = LinkText(notice, "Link", notice.DisplayName);
					}

					var dot = Field(notice, "DotColor");
					if (string.IsNullOrWhiteSpace(dot))
					{
						dot = "red";
					}

					model.Notices.Add(new NoticeItemModel
					{
						Item = notice,
						Date = Field(notice, "Date"),
						Text = text,
						DotClass = "dot-" + dot.Trim().ToLowerInvariant()
					});
				}
			}

			if (safetyFolder != null)
			{
				if (!string.IsNullOrWhiteSpace(Field(safetyFolder, "Title")))
				{
					model.SafetyHeading = Field(safetyFolder, "Title");
				}

				foreach (Item safety in safetyFolder.Children)
				{
					var title = Field(safety, "Title");
					var subtitle = Field(safety, "Subtitle");
					if (!IsExperienceEditor && string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(subtitle))
					{
						continue;
					}

					if (!IsExperienceEditor && string.IsNullOrWhiteSpace(title))
					{
						title = LinkText(safety, "Link", safety.DisplayName);
					}

					model.SafetyItems.Add(new SafetyItemModel
					{
						Item = safety,
						Icon = IconGlyph(Field(safety, "Icon")),
						Title = title,
						Subtitle = subtitle
					});
				}
			}

			return model;
		}

		private NavigationModel BuildNavigationModel()
		{
			var item = GetDatasource();
			var model = new NavigationModel
			{
				Item = item,
				Tabs = new List<NavigationTab>()
			};

			if (item == null)
			{
				return model;
			}

			foreach (Item tabItem in item.Children)
			{
				var tab = new NavigationTab
				{
					Item = tabItem,
					Label = LinkText(tabItem, "Link", tabItem.DisplayName),
					Url = LinkUrl(tabItem, "Link"),
					Links = new List<NavigationLink>(),
					Panels = new List<NavigationPanel>()
				};

				foreach (Item linkItem in tabItem.Children)
				{
					var hasChildren = linkItem.Children.Any();
					var navLink = new NavigationLink
					{
						Item = linkItem,
						Label = LinkText(linkItem, "Link", linkItem.DisplayName),
						Url = LinkUrl(linkItem, "Link"),
						IsExpandable = hasChildren,
						PanelKey = hasChildren ? SanitizeKey(linkItem.Name) : null
					};
					tab.Links.Add(navLink);

					if (hasChildren)
					{
						var panel = new NavigationPanel
						{
							Key = navLink.PanelKey,
							Links = linkItem.Children.Cast<Item>().Select(child => new NavigationLink
							{
								Item = child,
								Label = LinkText(child, "Link", child.DisplayName),
								Url = LinkUrl(child, "Link")
							}).ToList()
						};
						tab.Panels.Add(panel);
					}
				}

				model.Tabs.Add(tab);
			}

			return model;
		}

		private static string SanitizeKey(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return "panel";
			}

			var cleaned = new string(name.ToLowerInvariant().Where(char.IsLetterOrDigit).ToArray());
			return string.IsNullOrEmpty(cleaned) ? "panel" : cleaned;
		}
	}
}
