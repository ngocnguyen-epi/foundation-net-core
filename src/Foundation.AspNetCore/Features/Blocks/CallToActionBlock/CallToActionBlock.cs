using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Foundation.AspNetCore.Features.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.Blocks.CallToActionBlock
{
    [ContentType(DisplayName = "Call To Action Block",
        GUID = "f82da800-c923-48f6-b701-fd093078c5d9",
        Description = "Provides a CTA anchor or link",
        GroupName = GroupNames.Content)]
    [ImageUrl("/icons/cms/blocks/CMS-icon-block-26.png")]
    public class CallToActionBlock : FoundationBlockData/*, IDashboardItem*/
    {
        [CultureSpecific]
        [Display(Name = "Title", Description = "Title displayed", GroupName = SystemTabNames.Content, Order = 10)]
        public virtual string Title { get; set; }

        [CultureSpecific]
        [Display(GroupName = SystemTabNames.Content, Order = 20)]
        public virtual XhtmlString Subtext { get; set; }

        [Display(Name = "Text color", GroupName = SystemTabNames.Content, Order = 30)]
        public virtual string TextColor { get; set; }

        [UIHint(UIHint.Image)]
        [Display(Name = "Background image", GroupName = SystemTabNames.Content, Order = 40)]
        public virtual ContentReference BackgroundImage { get; set; }

        [SelectOne(SelectionFactoryType = typeof(BackgroundImageSelectionFactory))]
        [Display(Name = "Choose image style to fit the block", Order = 41, GroupName = SystemTabNames.Content)]
        public virtual string BackgroundImageSetting { get; set; }

        [Display(GroupName = SystemTabNames.Content, Order = 50)]
        public virtual ButtonBlock.ButtonBlock Button { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

            TextColor = "black";
            BackgroundImageSetting = "image-default";
        }

        //public void SetItem(ItemModel itemModel)
        //{
        //    itemModel.Description = Subtext?.ToHtmlString();
        //    itemModel.Image = BackgroundImage;
        //}
    }
}
