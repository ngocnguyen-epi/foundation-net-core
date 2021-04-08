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
            [Display(Name = "Contact", Order = 1)]
            public const string Contact = "Contact";

            [Display(Name = "Default", Order = 2)]
            public const string Default = "Default";

            [Display(Name = "Metadata", Order = 3)]
            public const string MetaData = "Metadata";

            [Display(Name = "News", Order = 4)]
            public const string News = "News";

            [Display(Name = "Products", Order = 5)]
            public const string Products = "Products";

            [Display(Name = "Site Settings", Order = 6)]
            public const string SiteSettings = "SiteSettings";

            [Display(Name = "Specialized", Order = 7)]
            public const string Specialized = "Specialized";

            [Display(Name = "Content", Order = 8)]
            public const string Content = "Content";

            [Display(Name = "Account", Order = 9)]
            public const string Account = "Account";

            [Display(Name = "Blog", Order = 10)]
            public const string Blog = "Blog";

            [Display(Name = "Calendar", Order = 11)]
            public const string Calendar = "Calendar";

            [Display(Name = "Forms", Order = 12)]
            public const string Forms = "Forms";

            [Display(Name = "Multimedia", Order = 13)]
            public const string Multimedia = "Multimedia";

            [Display(Name = "Social media", Order = 14)]
            public const string SocialMedia = "Social media";

            [Display(Name = "Syndication", Order = 15)]
            public const string Syndication = "Syndication";

            [Display(Name = "Blog list", Order = 16)]
            public const string BlogList = "BlogList";

            [Display(Name = "Review", Order = 17)]
            public const string Review = "Review";

            [Display(Name = "Header", Order = 18)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string Header = "Header";

            [Display(Name = "Footer", Order = 19)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string Footer = "Footer";

            [Display(Name = "Search settings", Order = 20)]
            public const string SearchSettings = "SearchSettings";

            [Display(Name = "Menu", Order = 21)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string Menu = "Menu";

            [Display(Name = "Site labels", Order = 22)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string SiteLabels = "SiteLabels";

            [Display(Name = "Manufacturer", Order = 23)]
            public const string Manufacturer = "Manufacturer";

            [Display(Name = "Site structure", Order = 24)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string SiteStructure = "SiteStructure";

            [Display(Name = "Mail templates", Order = 25)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string MailTemplates = "MailTemplates";

            [Display(Name = "Archives", Order = 26)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string Archives = "Archives";

            [Display(Name = "Tags", Order = 27)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string Tags = "Tags";

            [Display(Name = "Location", Order = 28)]
            public const string Location = "Location";

            [Display(Name = "Person", Order = 29)]
            public const string Person = "Person";

            [Display(Name = "Teaser", Order = 30)]
            public const string Teaser = "Teaser";

            [Display(Name = "Custom settings", Order = 31)]
            public const string CustomSettings = "CustomSettings";

            [Display(Name = "Styles", Order = 32)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string Styles = "Styles";

            [Display(Name = "Scripts", Order = 33)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string Scripts = "Scripts";

            [Display(Name = "Background", Order = 34)]
            public const string Background = "Background";

            [Display(Name = "Border", Order = 35)]
            public const string Border = "Border";

            [Display(Name = "Block styling", Order = 36)]
            public const string BlockStyling = "BlockStyling";

            [Display(Name = "Colors", Order = 37)]
            public const string Colors = "Colors";
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

