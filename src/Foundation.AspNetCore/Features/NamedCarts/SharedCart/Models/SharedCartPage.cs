using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Business.UIDescriptors;
using Foundation.AspNetCore.Features.Shared;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.NamedCarts.SharedCart.Models
{
    [ContentType(DisplayName = "Shared Cart Page",
        GUID = "701b5df0-fa41-40cb-807f-645be22714cc",
        Description = "Page to manage organization's shared cart.",
        GroupName = GroupNames.Commerce,
        AvailableInEditMode = false)]
    [ImageUrl("/icons/cms/pages/cms-icon-page-08.png")]
    public class SharedCartPage : FoundationPageData, IDisableOPE
    {
    }
}