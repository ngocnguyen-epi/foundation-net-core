using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Foundation.AspNetCore.Features.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.Blocks.ButtonBlock
{
    [ContentType(DisplayName = "Button Block",
        GUID = "426CF12F-1F01-4EA0-922F-0778314DDAF0",
        Description = "Used to insert a link which is styled as a button",
        GroupName = GroupNames.Content,
        AvailableInEditMode = true)]
    [ImageUrl("~/assets/icons/cms/blocks/CMS-icon-block-26.png")]
    public class ButtonBlock : FoundationBlockData
    {
        #region Content
        [CultureSpecific]
        [Display(Name = "Label", Order = 10, GroupName = SystemTabNames.Content)]
        public virtual string ButtonText { get; set; }

        [Display(Name = "Link", Order = 20, GroupName = SystemTabNames.Content)]
        public virtual Url ButtonLink { get; set; }

        [SelectOne(SelectionFactoryType = typeof(ButtonBlockStyleSelectionFactory))]
        [Display(Name = "Style", Order = 30, GroupName = SystemTabNames.Content)]
        public virtual string ButtonStyle { get; set; }

        [CultureSpecific]
        [Display(Name = "Reassuring caption", Order = 40, GroupName = SystemTabNames.Content, Prompt = "Cancel anytime...")]
        public virtual string ButtonCaption { get; set; }

        [CultureSpecific]
        [Searchable(false)]
        [ClientEditor(ClientEditingClass = "foundation/editors/ColorPicker")]
        [Display(Name = "Button Text color", GroupName = SystemTabNames.Content, Order = 50)]
        public virtual string ButtonTextColor
        {
            get { return this.GetPropertyValue(page => page.ButtonTextColor) ?? "#000000ff"; }
            set { this.SetPropertyValue(page => page.ButtonTextColor, value); }
        }

        #endregion

        #region Button Background
        [CultureSpecific]
        [Display(Name = "Use transparent background", GroupName = TabNames.Background,
            Description = "This will determine whether or not to use transparent background", Order = 10)]
        public virtual bool ShowTransparentBackground { get; set; }

        [CultureSpecific]
        [Searchable(false)]
        [ClientEditor(ClientEditingClass = "foundation/editors/ColorPicker")]
        [Display(Name = "Button background color", GroupName = TabNames.Background, Order = 20)]
        public virtual string ButtonBackgroundColor
        {
            get { return this.GetPropertyValue(page => page.ButtonBackgroundColor) ?? "#ffffffff"; }
            set { this.SetPropertyValue(page => page.ButtonBackgroundColor, value); }
        }
        #endregion

        #region Border
        [CultureSpecific]
        [Display(Name = "Border Styles", GroupName = TabNames.Border, Description = "This will determine whether or not to show border", Order = 10)]
        [SelectOne(SelectionFactoryType = typeof(BorderStyleSelectionFactory))]
        public virtual string BorderStyle { get; set; }

        [Display(Name = "Border width (px)", GroupName = TabNames.Border, Order = 20)]
        [RegularExpression("^[+]?\\d*$"
            , ErrorMessage = "BorderWidth must be non-negative")]
        public virtual int BorderWidth { get; set; }

        [CultureSpecific]
        [Searchable(false)]
        [ClientEditor(ClientEditingClass = "foundation/editors/ColorPicker")]
        [Display(Name = "Button Border color", GroupName = TabNames.Border, Order = 30)]
        public virtual string ButtonBorderColor
        {
            get { return this.GetPropertyValue(page => page.ButtonBorderColor) ?? "#ffffffff"; }
            set { this.SetPropertyValue(page => page.ButtonBorderColor, value); }
        }

        #endregion
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);
            ButtonBackgroundColor = "#ffffffff";
            ButtonTextColor = "#000000ff";
            ButtonBorderColor = "#000000ff";
            ShowTransparentBackground = false;
            BorderStyle = "none";
            BorderWidth = 1;
        }
    }
}
