using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Foundation.AspNetCore
{

    public class Global
    {
        public static readonly string LoginPath = "/util/login.aspx";
        public static readonly string AppRelativeLoginPath = string.Format("~{0}", LoginPath);

        /// <summary>
        /// Group names for content types and properties
        /// </summary>
        [GroupDefinitions()]
        public static class GroupNames
        {
            [Display(Name = "Content", Order = 510)]
            public const string Content = "Content";

            [Display(Name = "Commerce", Order = 510)]
            public const string Commerce = "Commerce";

            [Display(Order = 530)]
            public const string Account = "Account";

            [Display(Order = 540)]
            public const string Blog = "Blog";

            [Display(Name = "Calendar", Order = 550)]
            public const string Calendar = "Calendar";

            [Display(Order = 570)]
            public const string Forms = "Forms";

            [Display(Order = 580)]
            public const string Multimedia = "Multimedia";

            [Display(Order = 600)]
            public const string SocialMedia = "Social media";

            [Display(Order = 620)]
            public const string Syndication = "Syndication";
        }

        [GroupDefinitions]
        public static class TabNames
        {
            [Display(Order = 10)]
            public const string Default = "Default";

            [Display(Name = "Blog list", Order = 30)]
            public const string BlogList = "BlogList";

            [Display(Order = 40)]
            public const string Review = "Review";

            [Display(Order = 50)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string Header = "Header";

            [Display(Order = 60)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string Footer = "Footer";

            [Display(Name = "Search settings", Order = 65)]
            public const string SearchSettings = "SearchSettings";

            [Display(Order = 70)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string Menu = "Menu";

            [Display(Name = "Site labels", Order = 75)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string SiteLabels = "SiteLabels";

            [Display(Order = 76)]
            public const string Manufacturer = "Manufacturer";

            [Display(Name = "Site structure", Order = 77)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string SiteStructure = "SiteStructure";

            [Display(Name = "Mail templates", Order = 78)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string MailTemplates = "MailTemplates";

            [Display(Order = 80)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string Archives = "Archives";

            [Display(Order = 90)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string Tags = "Tags";

            [Display(Order = 100)]
            public const string Location = "Location";

            [Display(Order = 200)]
            public const string Person = "Person";

            [Display(Order = 250)]
            public const string Teaser = "Teaser";

            [Display(Order = 260)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string MetaData = "Metadata";

            [Display(Name = "Custom settings", Order = 265)]
            public const string CustomSettings = "CustomSettings";

            [Display(Order = 270)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string Styles = "Styles";

            [Display(Order = 280)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string Scripts = "Scripts";

            [Display(Name = "Background", Order = 283)]
            public const string Background = "Background";

            [Display(Name = "Border", Order = 284)]
            public const string Border = "Border";

            [Display(Name = "Block styling", Order = 285)]
            public const string BlockStyling = "BlockStyling";

            [Display(Name = "Colors", Order = 289)]
            public const string Colors = "Colors";

            [Display(Name = "Settings", Order = 290)]
            public const string Settings = SystemTabNames.Settings;
        }

        /// <summary>
        /// Tags to use for the main widths used in the Bootstrap HTML framework
        /// </summary>
        public static class ContentAreaTags
        {
            public const string FullWidth = "col-12";
            public const string TwoThirdsWidth = "col-8";
            public const string HalfWidth = "col-6";
            public const string OneThirdWidth = "col-4";
            public const string NoRenderer = "norenderer";
        }

        /// <summary>
        /// Main widths used in the Bootstrap HTML framework
        /// </summary>
        public static class ContentAreaWidths
        {
            public const int FullWidth = 12;
            public const int TwoThirdsWidth = 8;
            public const int HalfWidth = 6;
            public const int OneThirdWidth = 4;
        }

        public static Dictionary<string, int> ContentAreaTagWidths = new Dictionary<string, int>
            {
                { ContentAreaTags.FullWidth, ContentAreaWidths.FullWidth },
                { ContentAreaTags.TwoThirdsWidth, ContentAreaWidths.TwoThirdsWidth },
                { ContentAreaTags.HalfWidth, ContentAreaWidths.HalfWidth },
                { ContentAreaTags.OneThirdWidth, ContentAreaWidths.OneThirdWidth }
            };

        /// <summary>
        /// Names used for UIHint attributes to map specific rendering controls to page properties
        /// </summary>
        public static class SiteUIHints
        {
            public const string Contact = "contact";
            public const string Strings = "StringList";
            public const string StringsCollection = "StringsCollection";
        }

        /// <summary>
        /// Virtual path to folder with static graphics, such as "/gfx/"
        /// </summary>
        public const string StaticGraphicsFolderPath = "/gfx/";
    }
}

