using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Features.MyAccount.ResetPassword.Models;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.CheckoutFeatures.ConfirmationMail.Models
{
    [ContentType(DisplayName = "Order Confirmation Mail Page",
        GUID = "f13b7a68-0702-4023-92b3-15064d338c0c",
        Description = "The reset passord template mail page.",
        GroupName = GroupNames.Commerce,
        AvailableInEditMode = false)]
    [ImageUrl("/icons/cms/pages/CMS-icon-page-26.png")]
    public class OrderConfirmationMailPage : MailBasePage
    {
    }
}