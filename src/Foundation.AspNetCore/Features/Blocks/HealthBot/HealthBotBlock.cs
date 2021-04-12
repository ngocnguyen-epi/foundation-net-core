using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Features.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.Blocks.HealthBot
{
    [ContentType(DisplayName = "Health chatbot",
        GUID = "18A7B10E-451C-4223-BAD0-36BD224E3927",
        Description = "Used to insert a health chat bot",
        GroupName = GroupNames.Content,
        AvailableInEditMode = true)]
    [ImageUrl("/icons/cms/blocks/CMS-icon-block-25.png")]
    public class HealthBotBlock : FoundationBlockData
    {
        [CultureSpecific]
        [Display(
            Name = "Text above bot",
            Description = "Text that appears above the chat bot",
            Order = 10,
            GroupName = SystemTabNames.Content)]
        public virtual XhtmlString HeaderText { get; set; }

        [Display(
            Name = "Direct Line Token",
            Description = "The token that is used to connect to the bot framework. Get this from > Health Bot Service > Integration > Channels > DirectLine",
            Order = 10,
            GroupName = "Bot Configuration")]
        [Required]
        public virtual string DirectLineToken { get; set; }

        [Display(
            Name = "Height (in pixels)",
            Description = "The height of the bot in pixels as shown on screen",
            Order = 10,
            GroupName = "Presentation")]
        [Range(100, 5000)]
        public virtual int HeightInPixels { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

            HeightInPixels = 300;
        }
    }
}
