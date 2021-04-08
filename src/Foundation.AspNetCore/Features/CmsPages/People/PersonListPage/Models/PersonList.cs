using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Foundation.AspNetCore;
using Foundation.AspNetCore.Features.Shared;
using Foundation.Features.CmsPages.People.PersonItemPage;

namespace Foundation.Features.CmsPages.People.PersonListPage
{
    [ContentType(DisplayName = "Person List Page",
        GUID = "4f0203b6-d49e-4683-9ce6-ede8c37c77d3",
        Description = "Used to find people within an organization",
        GroupName = Global.TabNames.Person)]
    [AvailableContentTypes(Availability.Specific, Include = new[] { typeof(PersonList), typeof(PersonPage) })]
    [ImageUrl("/icons/cms/pages/contactcatalogue.png")]
    public class PersonList : FoundationPageData
    {
    }
}