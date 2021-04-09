using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Features.CmsPages.Location.Blocks.FilterActivitiesBlock;
using Foundation.AspNetCore.Features.CmsPages.Location.Blocks.FilterContinentsBlock;
using Foundation.AspNetCore.Features.CmsPages.Location.Blocks.FilterDistancesBlock;
using Foundation.AspNetCore.Features.CmsPages.Location.Blocks.FilterTemperaturesBlock;
using Foundation.AspNetCore.Features.Shared;
using System.ComponentModel.DataAnnotations;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.CmsPages.Location.LocationListPage.Models
{
    [ContentType(DisplayName = "Locations List Page",
        GUID = "597afd14-391b-4e99-8e4f-8827e3e82354",
        Description = "Used to display a list of all locations",
        GroupName = TabNames.Location)]
    [ImageUrl("/icons/cms/pages/cms-icon-page-27.png")]
    [AvailableContentTypes(Availability = Availability.Specific, Include = new[] { typeof(LocationItemPage.Models.LocationItemPage) })]
    public class LocationListPage : FoundationPageData
    {
        [AllowedTypes(new[] { typeof(FilterActivitiesBlock), typeof(FilterContinentsBlock), typeof(FilterDistancesBlock), typeof(FilterTemperaturesBlock) })]
        [Display(Name = "Filter area", GroupName = SystemTabNames.Content, Order = 210)]
        public virtual ContentArea FilterArea { get; set; }
    }
}