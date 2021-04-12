using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Foundation.AspNetCore.Features.Shared;
using Foundation.AspNetCore.Features.Shared.SelectionFactories;
using System.ComponentModel.DataAnnotations;

namespace Foundation.AspNetCore.Features.CmsPages.People.PersonItemPage.Models
{
    [ContentType(DisplayName = "Person Item Page",
        GUID = "b5af511b-96c9-4ad7-828f-254924542430",
        Description = "Used to show info of specific person",
        GroupName = Global.TabNames.Person)]
    [AvailableContentTypes(Availability.Specific, Exclude = new[] { typeof(PageData) })]
    [ImageUrl("/icons/cms/blocks/contact.png")]
    public class PersonPage : FoundationPageData
    {
        [CultureSpecific]
        [Display(Name = "Job title", GroupName = SystemTabNames.Content, Order = 1)]
        public virtual string JobTitle { get; set; }

        [CultureSpecific]
        [Display(GroupName = SystemTabNames.Content, Order = 2)]
        [SelectOne(SelectionFactoryType = typeof(LocationsSelectionFactory))]
        public virtual string Location { get; set; }

        [CultureSpecific]
        [Display(GroupName = SystemTabNames.Content, Order = 3)]
        [SelectOne(SelectionFactoryType = typeof(SectorsSelectionFactory))]
        public virtual string Sector { get; set; }

        [Display(GroupName = SystemTabNames.Content, Order = 4)]
        public virtual string Phone { get; set; }

        [CultureSpecific]
        [Display(GroupName = SystemTabNames.Content, Order = 5)]
        public virtual string Email { get; set; }

        [UIHint(UIHint.Image)]
        [Display(Name = "Person image", GroupName = SystemTabNames.Content, Order = 6)]
        public virtual ContentReference Image { get; set; }

        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        [Display(GroupName = SystemTabNames.Content, Order = 7)]
        public virtual XhtmlString About { get; set; }
    }
}