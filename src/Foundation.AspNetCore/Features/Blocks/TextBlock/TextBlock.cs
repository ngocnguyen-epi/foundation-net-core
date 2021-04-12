using EPiServer.Core;
using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Features.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.Blocks.TextBlock
{
    [ContentType(DisplayName = "Text Block",
        GUID = "32782B29-278B-410A-A402-9FF46FAF32B9",
        Description = "Simple Rich Text Block",
        GroupName = GroupNames.Content)]
    [ImageUrl("/icons/cms/blocks/CMS-icon-block-03.png")]
    public class TextBlock : FoundationBlockData
    {
        [CultureSpecific]
        [Display(Name = "Main body")]
        public virtual XhtmlString MainBody { get; set; }
    }
}
