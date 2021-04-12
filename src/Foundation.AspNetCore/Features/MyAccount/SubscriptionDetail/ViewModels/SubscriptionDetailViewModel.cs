using Foundation.AspNetCore.Features.MyAccount.OrderHistory.ViewModels;
using Foundation.AspNetCore.Features.MyAccount.SubscriptionDetail.Models;
using Foundation.AspNetCore.Features.Shared;
using Mediachase.Commerce.Orders;

namespace Foundation.AspNetCore.Features.MyAccount.SubscriptionDetail.ViewModels
{
    public class SubscriptionDetailViewModel : ContentViewModel<SubscriptionDetailPage>
    {
        public SubscriptionDetailViewModel(SubscriptionDetailPage currentPage) : base(currentPage)
        {
        }

        public OrderHistoryViewModel Orders { get; set; }
        public PaymentPlan PaymentPlan { get; set; }
    }
}