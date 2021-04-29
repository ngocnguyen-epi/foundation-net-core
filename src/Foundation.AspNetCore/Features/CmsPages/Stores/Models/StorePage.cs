using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Features.Shared;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.CmsPages.Stores.Models
{
    [ContentType(DisplayName = "Store Page",
        GUID = "77cf19e8-9a94-4c5b-a9be-ece53de563dc",
        Description = "Store locator page.",
        GroupName = GroupNames.Commerce,
        AvailableInEditMode = false)]
    [ImageUrl("/icons/cms/pages/CMS-icon-page-22.png")]
    public class StorePage : FoundationPageData
    {
    }
}