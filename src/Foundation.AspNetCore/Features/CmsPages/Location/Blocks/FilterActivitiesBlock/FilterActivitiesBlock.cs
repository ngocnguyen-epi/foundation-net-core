using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Features.Shared;
using System;
using System.ComponentModel.DataAnnotations;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.CmsPages.Location.Blocks.FilterActivitiesBlock
{
    [ContentType(DisplayName = "Filter Activities Block",
        GUID = "918c590e-b2cd-4b87-9116-899b1db19117",
        Description = "Activity facets for locations",
        GroupName = TabNames.Location)]
    [ImageUrl("/icons/cms/blocks/map.png")]
    [AvailableContentTypes(Include = new Type[] { typeof(LocationListPage.Models.LocationListPage) })]
    public class FilterActivitiesBlock : FoundationBlockData, IFilterBlock
    {
        [CultureSpecific]
        [Display(Name = "Filter title")]
        public virtual string FilterTitle { get; set; }

        [CultureSpecific]
        [Display(Name = "All condition text")]
        public virtual string AllConditionText { get; set; }
    }
}