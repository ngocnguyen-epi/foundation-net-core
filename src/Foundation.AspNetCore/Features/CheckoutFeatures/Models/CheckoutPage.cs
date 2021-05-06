using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Features.CmsPages.Home;
using Foundation.AspNetCore.Features.MyAccount.OrderConfirmation.Models;
using Foundation.AspNetCore.Features.Shared;
using static Foundation.AspNetCore.Infrastructure.Constant;

namespace Foundation.AspNetCore.Features.CheckoutFeatures.Models
{
    [ContentType(DisplayName = "Checkout Page",
        GUID = "6709cd32-7bb6-4d29-9b0b-207369799f4f",
        Description = "Checkout page",
        GroupName = GroupNames.Commerce,
        AvailableInEditMode = false)]
    [AvailableContentTypes(Include = new[] { typeof(OrderConfirmationPage) }, IncludeOn = new[] { typeof(HomePage) })]
    [ImageUrl("/icons/cms/pages/cms-icon-page-08.png")]
    public class CheckoutPage : FoundationPageData
    {
    }
}