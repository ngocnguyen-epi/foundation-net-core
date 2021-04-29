using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Features.Shared;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.MyOrganization.Orders.Models
{
    [ContentType(DisplayName = "Organization Orders Page",
        GUID = "3c436a14-38d1-4fd1-ab37-15f0848cfa24",
        Description = "Page to manage an organizations's orders",
        GroupName = GroupNames.Commerce)]
    [ImageUrl("/icons/cms/pages/cms-icon-page-15.png")]
    public class OrdersPage : FoundationPageData
    {
    }
}