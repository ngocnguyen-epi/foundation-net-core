using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Foundation.AspNetCore.Cms.Settings;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.Settings
{
    [SettingsContentType(DisplayName = "Layout Settings",
        GUID = "f7366060-c801-494c-99b8-b761ac3447c3",
        Description = "Header settings, footer settings, menu settings",
        AvailableInEditMode = true,
        SettingsName = "Layout Settings")]
    public class LayoutSettings : SettingsBase
    {
        #region Footer

        [Display(Name = "Introduction", GroupName = Global.TabNames.Footer, Order = 10)]
        public virtual string Introduction { get; set; }

        [Display(Name = "Company header", GroupName = Global.TabNames.Footer, Order = 20)]
        public virtual string CompanyHeader { get; set; }

        [Display(Name = "Company name", GroupName = Global.TabNames.Footer, Order = 25)]
        public virtual string CompanyName { get; set; }

        [Display(Name = "Comapny address", GroupName = Global.TabNames.Footer, Order = 30)]
        public virtual string CompanyAddress { get; set; }

        [Display(Name = "Company phone", GroupName = Global.TabNames.Footer, Order = 40)]
        public virtual string CompanyPhone { get; set; }

        [Display(Name = "Company email", GroupName = Global.TabNames.Footer, Order = 50)]
        public virtual string CompanyEmail { get; set; }

        [Display(Name = "Links header", GroupName = Global.TabNames.Footer, Order = 60)]
        public virtual string LinksHeader { get; set; }

        [UIHint("FooterColumnNavigation")]
        [Display(Name = "Links", GroupName = Global.TabNames.Footer, Order = 70)]
        public virtual LinkItemCollection Links { get; set; }

        [Display(Name = "Social header", GroupName = Global.TabNames.Footer, Order = 80)]
        public virtual string SocialHeader { get; set; }

        [Display(Name = "Social links", GroupName = Global.TabNames.Footer, Order = 85)]
        public virtual LinkItemCollection SocialLinks { get; set; }

        [CultureSpecific]
        [Display(Name = "Content area", GroupName = Global.TabNames.Footer, Order = 90)]
        public virtual ContentArea ContentArea { get; set; }

        [Display(Name = "Copyright", GroupName = Global.TabNames.Footer, Order = 130)]
        public virtual string FooterCopyrightText { get; set; }

        #endregion

        #region Menu   

        [CultureSpecific]
        //[AllowedTypes(new[] { typeof(MenuItemBlock), typeof(PageData) })]
        //[UIHint("HideContentAreaActionsContainer", PresentationLayer.Edit)]
        [Display(Name = "Main menu", GroupName = TabNames.Menu, Order = 10)]
        public virtual ContentArea MainMenu { get; set; }

        [CultureSpecific]
        [Display(Name = "My account menu",
            GroupName = TabNames.Menu,
            Order = 40)]
        public virtual LinkItemCollection MyAccountCmsMenu { get; set; }

        [CultureSpecific]
        [Display(Name = "Organization menu", GroupName = TabNames.Menu, Order = 50)]
        public virtual LinkItemCollection OrganizationMenu { get; set; }

        #endregion

        #region Header

        [CultureSpecific]
        [UIHint(UIHint.Image)]
        [Display(Name = "Site logo", GroupName = Global.TabNames.Header, Order = 10)]
        public virtual ContentReference SiteLogo { get; set; }

        [Display(Name = "Logo height (pixels)", GroupName = Global.TabNames.Header, Order = 15)]
        public virtual int LogoHeight { get; set; }

        [SelectOne(SelectionFactoryType = typeof(BackgroundColorSelectionFactory))]
        [Display(Name = "Header Background Color", GroupName = Global.TabNames.Header, Order = 15)]
        public virtual string HeaderBackgroundColor { get; set; }

        [CultureSpecific]
        [SelectOne(SelectionFactoryType = typeof(HeaderMenuSelectionFactory))]
        [Display(Name = "Menu style", GroupName = Global.TabNames.Header, Order = 30)]
        public virtual string HeaderMenuStyle { get; set; }

        [CultureSpecific]
        [Display(Name = "Large header menu", GroupName = Global.TabNames.Header, Order = 35)]
        public virtual bool LargeHeaderMenu { get; set; }

        [CultureSpecific]
        [Display(Name = "Show commerce header components", GroupName = TabNames.Header, Order = 40)]
        public virtual bool ShowCommerceHeaderComponents { get; set; }

        [CultureSpecific]
        [Display(Name = "Sticky header", GroupName = Global.TabNames.Header, Order = 50)]
        public virtual bool StickyTopHeader { get; set; }

        [CultureSpecific]
        [Display(Name = "Banner text", GroupName = Global.TabNames.Header, Order = 20)]
        public virtual XhtmlString BannerText { get; set; }

        #endregion

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            LargeHeaderMenu = false;
            LogoHeight = 50;
        }
    }

    public class HeaderMenuSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new ISelectItem[]
            {
                new SelectItem {Text = "Center logo", Value = "CenterLogo"},
                new SelectItem {Text = "Left logo", Value = "LeftLogo"}
            };
        }
    }
}