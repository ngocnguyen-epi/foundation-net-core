using EPiServer.DataAnnotations;
using Foundation.AspNetCore.Features.Shared;
using static Foundation.AspNetCore.Global;

namespace Foundation.AspNetCore.Features.MyAccount.SubscriptionHistory.Models
{
    [ContentType(DisplayName = "Subscription History",
        GUID = "9770edaf-2da0-4522-a446-302d084975c1",
        Description = "Page for customers to view their subscription history",
        GroupName = GroupNames.Commerce,
        AvailableInEditMode = false)]
    [ImageUrl("~/icons/cms/pages/CMS-icon-page-14.png")]
    public class SubscriptionHistoryPage : FoundationPageData
    {
    }
}