using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Features.Shared;
using System.ComponentModel.DataAnnotations;

namespace Foundation.AspNetCore.Features.CmsPages.LandingPages.LandingPage.Models
{
    [ContentType(DisplayName = "Single Column Landing Page",
       GUID = "DBED4258-8213-48DB-A11F-99C034172A54",
       Description = "Default standard page that has top content area, main body, and main content area",
       GroupName = Global.GroupNames.Content)]
    [ImageUrl("/icons/gfx/page-type-thumbnail-landingpage-onecol.png")]
    public class LandingPage : FoundationPageData
    {
        [Display(Name = "Top content area", GroupName = SystemTabNames.Content, Order = 90)]
        public virtual ContentArea TopContentArea { get; set; }
    }
}