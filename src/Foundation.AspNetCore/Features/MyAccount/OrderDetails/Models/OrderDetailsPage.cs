using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Features.Shared;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.MyAccount.OrderDetails.Models
{
    [ContentType(DisplayName = "Order Details Page",
        GUID = "11ad9718-fc02-45d0-9b98-349da9493dce",
        Description = "Page for customer to view their order",
        GroupName = GroupNames.Commerce,
        AvailableInEditMode = false)]
    [ImageUrl("~/assets/icons/cms/pages/cms-icon-page-15.png")]
    public class OrderDetailsPage : FoundationPageData
    {
    }
}