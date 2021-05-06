using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Business.UIDescriptors;
using Foundation.AspNetCore.Features.Shared;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.NamedCarts.OrderPadsPage.Models
{
    [ContentType(DisplayName = "Order Pads Page",
        GUID = "32114883-3ebb-4582-b864-7262ea177af0",
        Description = "Page to manage an organization member's order pad",
        GroupName = GroupNames.Commerce,
        AvailableInEditMode = false)]
    [ImageUrl("/icons/cms/pages/cms-icon-page-15.png")]
    public class OrderPadsPage : FoundationPageData, IDisableOPE
    {
    }
}