using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Features.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.Blocks.GoogleMapsBlock
{
    [ContentType(DisplayName = "Google Maps Block",
        GUID = "8fc31051-6d22-4445-b92d-7c394267fa49",
        Description = "Display Google Maps",
        GroupName = GroupNames.SocialMedia)]
    [ImageUrl("~/assets/icons/cms/blocks/map.png")]
    public class GoogleMapsBlock : FoundationBlockData
    {
        [Required]
        [Display(Name = "API Key")]
        public virtual string ApiKey { get; set; }

        [Display(Name = "Search term")]
        public virtual string SearchTerm { get; set; }

        public virtual double Height { get; set; }
    }
}
