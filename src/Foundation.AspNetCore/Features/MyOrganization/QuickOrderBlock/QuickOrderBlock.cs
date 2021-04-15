using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Features.Shared;
using System.ComponentModel.DataAnnotations;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.MyOrganization.QuickOrderBlock
{
    [ContentType(DisplayName = "Quick Order Block",
        GUID = "003076FD-659C-485E-9480-254A447CC809",
        Description = "Used to quick order a list of products",
        GroupName = GroupNames.Commerce)]
    [ImageUrl("/icons/cms/pages/cms-icon-page-14.png")]
    public class QuickOrderBlock : FoundationBlockData
    {
        [CultureSpecific]
        [Display(GroupName = SystemTabNames.Content, Order = 5)]
        public virtual string Title { get; set; }

        [CultureSpecific]
        [Display(Name = "Main body", GroupName = SystemTabNames.Content, Order = 10)]
        public virtual XhtmlString MainBody { get; set; }
    }
}