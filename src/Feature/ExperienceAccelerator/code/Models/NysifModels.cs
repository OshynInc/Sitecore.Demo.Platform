using System.Collections.Generic;
using Sitecore.Data.Items;

namespace Sitecore.Demo.Platform.Feature.ExperienceAccelerator.Models
{
	public class HeroModel
	{
		public Item Item { get; set; }
		public string Supertitle { get; set; }
		public string TitleHtml { get; set; }
		public string Text { get; set; }
		public string PrimaryCtaUrl { get; set; }
		public string PrimaryCtaText { get; set; }
		public string SecondaryCtaUrl { get; set; }
		public string SecondaryCtaText { get; set; }
		public List<HeroStat> Stats { get; set; }
	}

	public class HeroStat
	{
		public string Value { get; set; }
		public string Label { get; set; }
	}

	public class ServiceAlertsModel
	{
		public Item Item { get; set; }
		public string Badge { get; set; }
		public List<ServiceAlertItem> Alerts { get; set; }
	}

	public class ServiceAlertItem
	{
		public Item Item { get; set; }
		public string Date { get; set; }
		public string Text { get; set; }
	}

	public class RoleCardsModel
	{
		public Item Item { get; set; }
		public string Eyebrow { get; set; }
		public string Title { get; set; }
		public string Subtitle { get; set; }
		public List<RoleCardItem> Cards { get; set; }
	}

	public class RoleCardItem
	{
		public Item Item { get; set; }
		public string Icon { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
	}

	public class TestimonialModel
	{
		public Item Item { get; set; }
		public string Eyebrow { get; set; }
		public string Title { get; set; }
		public List<TestimonialCardItem> Cards { get; set; }
	}

	public class TestimonialCardItem
	{
		public Item Item { get; set; }
		public string Quote { get; set; }
		public string Name { get; set; }
		public string Role { get; set; }
		public string Initials { get; set; }
	}

	public class PromoModel
	{
		public Item Item { get; set; }
		public string Icon { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string LinkUrl { get; set; }
		public string LinkText { get; set; }
	}

	public class QuickActionsModel
	{
		public Item Item { get; set; }
		public string Heading { get; set; }
		public string Subheading { get; set; }
		public List<PromoModel> Cards { get; set; }
	}

	public class PageListModel
	{
		public Item Item { get; set; }
		public string Heading { get; set; }
		public string Subheading { get; set; }
		public List<NewsItemModel> Items { get; set; }
	}

	public class NewsItemModel
	{
		public Item Item { get; set; }
		public string Title { get; set; }
		public string Summary { get; set; }
		public string Category { get; set; }
		public string PublishDate { get; set; }
		public string Url { get; set; }
	}

	public class NavigationModel
	{
		public Item Item { get; set; }
		public List<NavigationTab> Tabs { get; set; }
	}

	public class NavigationTab
	{
		public Item Item { get; set; }
		public string Label { get; set; }
		public string Url { get; set; }
		public List<NavigationLink> Links { get; set; }
		public List<NavigationPanel> Panels { get; set; }
	}

	public class NavigationLink
	{
		public Item Item { get; set; }
		public string Label { get; set; }
		public string Url { get; set; }
		public bool IsExpandable { get; set; }
		public string PanelKey { get; set; }
	}

	public class NavigationPanel
	{
		public string Key { get; set; }
		public List<NavigationLink> Links { get; set; }
	}

	public class NewsSidebarModel
	{
		public Item Item { get; set; }
		public string NoticesHeading { get; set; }
		public List<NoticeItemModel> Notices { get; set; }
		public string SafetyHeading { get; set; }
		public List<SafetyItemModel> SafetyItems { get; set; }
	}

	public class NoticeItemModel
	{
		public Item Item { get; set; }
		public string Date { get; set; }
		public string Text { get; set; }
		public string DotClass { get; set; }
	}

	public class SafetyItemModel
	{
		public Item Item { get; set; }
		public string Icon { get; set; }
		public string Title { get; set; }
		public string Subtitle { get; set; }
	}
}
