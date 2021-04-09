using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Foundation.AspNetCore.Features.Settings;
using Foundation.AspNetCore.Features.Shared.SelectionFactories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Foundation.AspNetCore.Features.Shared
{
    public class FoundationPageData : PageData, IFoundationContent
    {
        #region Page Header
        //[Categories]
        //[Display(Name = "Categories",
        //    Description = "Categories associated with this content.",
        //    GroupName = SystemTabNames.PageHeader,
        //    Order = 10)]
        //public virtual IList<ContentReference> Categories { get; set; }

        #endregion

        #region Content

        [CultureSpecific]
        [Display(Name = "Main body", GroupName = SystemTabNames.Content, Order = 100)]
        public virtual XhtmlString MainBody { get; set; }

        [CultureSpecific]
        [Display(Name = "Main content area", GroupName = SystemTabNames.Content, Order = 200)]
        public virtual ContentArea MainContentArea { get; set; }

        #endregion

        #region Metadata

        [CultureSpecific]
        [Display(Name = "Title", GroupName = Global.TabNames.MetaData, Order = 100)]
        public virtual string MetaTitle
        {
            get
            {
                var metaTitle = this.GetPropertyValue(p => p.MetaTitle);

                return !string.IsNullOrWhiteSpace(metaTitle)
                    ? metaTitle
                    : PageName;
            }
            set => this.SetPropertyValue(p => p.MetaTitle, value);
        }

        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        [Display(GroupName = Global.TabNames.MetaData, Order = 200)]
        public virtual string Keywords { get; set; }

        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        [Display(Name = "Page description", GroupName = Global.TabNames.MetaData, Order = 300)]
        public virtual string PageDescription { get; set; }

        [CultureSpecific]
        [Display(Name = "Content type", GroupName = Global.TabNames.MetaData, Order = 310)]
        public virtual string MetaContentType { get; set; }

        [CultureSpecific]
        [Display(Name = "Industry", GroupName = Global.TabNames.MetaData, Order = 320)]
        public virtual string Industry { get; set; }

        [CultureSpecific]
        [Display(Name = "Author", GroupName = Global.TabNames.MetaData, Order = 320)]
        public virtual string AuthorMetaData { get; set; }

        [CultureSpecific]
        [Display(Name = "Disable indexing", GroupName = Global.TabNames.MetaData, Order = 400)]
        public virtual bool DisableIndexing { get; set; }

        #endregion

        #region Settings

        [CultureSpecific]
        [Display(Name = "Exclude from results",
            Description = "This will determine whether or not to show on search",
            GroupName = SystemTabNames.Settings,
            Order = 100)]
        public virtual bool ExcludeFromSearch { get; set; }

        [CultureSpecific]
        [Display(Name = "Hide site header", GroupName = SystemTabNames.Settings, Order = 200)]
        public virtual bool HideSiteHeader { get; set; }

        [CultureSpecific]
        [Display(Name = "Hide site footer", GroupName = SystemTabNames.Settings, Order = 300)]
        public virtual bool HideSiteFooter { get; set; }

        [CultureSpecific]
        [Display(Name = "Highlight in page list", GroupName = SystemTabNames.Settings, Order = 400)]
        public virtual bool Highlight { get; set; }

        #endregion

        #region Teaser

        [CultureSpecific]
        [Searchable(false)]
        [SelectOne(SelectionFactoryType = typeof(BlockRatioSelectionFactory))]
        [Display(Name = "Teaser ratio (width-height)", GroupName = Global.TabNames.Teaser, Order = 50)]
        public virtual string TeaserRatio { get; set; }

        [UIHint(UIHint.Image)]
        [Display(Name = "Image", GroupName = Global.TabNames.Teaser, Order = 100)]
        public virtual ContentReference PageImage { get; set; }

        [CultureSpecific]
        [UIHint(UIHint.Video)]
        [Display(Name = "Video", GroupName = Global.TabNames.Teaser, Order = 200)]
        public virtual ContentReference TeaserVideo { get; set; }

        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        [Display(Name = "Text", GroupName = Global.TabNames.Teaser, Order = 300)]
        public virtual string TeaserText
        {
            get
            {
                var teaserText = this.GetPropertyValue(p => p.TeaserText);

                // Use explicitly set teaser text, otherwise fall back to description
                return !string.IsNullOrWhiteSpace(teaserText)
                    ? teaserText
                    : PageDescription;
            }
            set => this.SetPropertyValue(p => p.TeaserText, value);
        }

        [CultureSpecific]
        [Searchable(false)]
        [SelectOne(SelectionFactoryType = typeof(TeaserTextAlignmentSelectionFactory))]
        [Display(Name = "Text alignment", GroupName = Global.TabNames.Teaser, Order = 400)]
        public virtual string TeaserTextAlignment { get; set; }

        [CultureSpecific]
        [Searchable(false)]
        [SelectOne(SelectionFactoryType = typeof(TeaserColorThemeSelectionFactory))]
        [Display(Name = "Color theme", GroupName = Global.TabNames.Teaser, Order = 500)]
        public virtual string TeaserColorTheme { get; set; }

        [CultureSpecific]
        [Display(Name = "Button label", GroupName = Global.TabNames.Teaser, Order = 600)]
        public virtual string TeaserButtonText { get; set; }

        [CultureSpecific]
        [Searchable(false)]
        [SelectOne(SelectionFactoryType = typeof(ButtonBlockStyleSelectionFactory))]
        [Display(Name = "Button theme", GroupName = Global.TabNames.Teaser, Order = 700)]
        public virtual string TeaserButtonStyle { get; set; }

        [CultureSpecific]
        [Searchable(false)]
        [SelectOne(SelectionFactoryType = typeof(ButtonBackgroundColorSelectionFactory))]
        [Display(Name = "Button color", GroupName = Global.TabNames.Teaser, Order = 700)]
        public virtual string TeaserButtonColor { get; set; }

        [CultureSpecific]
        [Searchable(false)]
        [SelectOne(SelectionFactoryType = typeof(ButtonTextSelectionFactory))]
        [Display(Name = "Button text color", GroupName = Global.TabNames.Teaser, Order = 700)]
        public virtual string TeaserButtonTextColor { get; set; }

        [CultureSpecific]
        [Searchable(false)]
        [Display(Name = "Display hover effect", GroupName = Global.TabNames.Teaser, Order = 800)]
        public virtual bool ApplyHoverEffect { get; set; }

        [Searchable(false)]
        [SelectOne(SelectionFactoryType = typeof(PaddingSelectionFactory))]
        [Display(Name = "Padding", GroupName = Global.TabNames.Teaser, Order = 900)]
        public virtual string Padding
        {
            get { return this.GetPropertyValue(teaser => teaser.Padding) ?? "p-0"; }
            set { this.SetPropertyValue(teaser => teaser.Padding, value); }
        }

        [Searchable(false)]
        [SelectOne(SelectionFactoryType = typeof(MarginSelectionFactory))]
        [Display(Name = "Margin", GroupName = Global.TabNames.Teaser, Order = 910)]
        public virtual string Margin
        {
            get { return this.GetPropertyValue(teaser => teaser.Margin) ?? "m-0"; }
            set { this.SetPropertyValue(teaser => teaser.Margin, value); }
        }

        [Ignore]
        public string AlignmentCssClass
        {
            get
            {
                string alignmentClass;
                switch (TeaserTextAlignment)
                {
                    case "Left":
                        alignmentClass = "teaser-content-align--left";
                        break;
                    case "Right":
                        alignmentClass = "teaser-content-align--right";
                        break;
                    case "Center":
                        alignmentClass = "teaser-content-align--center";
                        break;
                    default:
                        alignmentClass = string.Empty;
                        break;
                }

                return alignmentClass;
            }
        }

        [Ignore]
        public string ThemeCssClass
        {
            get
            {
                string themeCssClass;
                switch (TeaserColorTheme)
                {
                    case "Light":
                        themeCssClass = "teaser-theme--light";
                        break;
                    case "Dark":
                        themeCssClass = "teaser-theme--dark";
                        break;
                    default:
                        themeCssClass = null;
                        break;
                }

                return themeCssClass;
            }
        }

        #endregion

        #region Styles

        [Display(Name = "CSS files", GroupName = Global.TabNames.Styles, Order = 100)]
        public virtual LinkItemCollection CssFiles { get; set; }

        [Display(Name = "CSS", GroupName = Global.TabNames.Styles, Order = 200)]
        [UIHint(UIHint.Textarea)]
        public virtual string Css { get; set; }

        #endregion

        #region Scripts

        [Display(Name = "Script files", GroupName = Global.TabNames.Scripts, Order = 100)]
        public virtual LinkItemCollection ScriptFiles { get; set; }

        [UIHint(UIHint.Textarea)]
        [Display(GroupName = Global.TabNames.Scripts, Order = 200)]
        public virtual string Scripts { get; set; }

        #endregion

        public override void SetDefaultValues(ContentType contentType)
        {
            TeaserTextAlignment = "Left";
            TeaserColorTheme = "Light";
            TeaserRatio = "2:1";
            TeaserButtonStyle = ButtonBlockStyles.TransparentWhite;
            TeaserButtonText = "Read more";
            ApplyHoverEffect = true;
            Padding = "p-1";
            Margin = "m-1";
            base.SetDefaultValues(contentType);
        }
    }
}
