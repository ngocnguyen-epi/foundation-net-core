using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Features.Shared;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.MyAccount.OrderHistory.Models
{
    [ContentType(DisplayName = "Order History Page",
        GUID = "6b950185-7270-43bf-90e5-fc57cc0d1b5c",
        Description = "Page for customer to view their order history.",
        GroupName = GroupNames.Commerce,
        AvailableInEditMode = false)]
    [ImageUrl("~/icons/cms/pages/cms-icon-page-15.png")]
    public class OrderHistoryPage : FoundationPageData
    {
    }
}