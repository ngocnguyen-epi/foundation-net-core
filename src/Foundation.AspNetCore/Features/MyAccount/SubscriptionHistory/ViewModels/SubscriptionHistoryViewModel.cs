using Foundation.AspNetCore.Features.MyAccount.SubscriptionHistory.Models;
using Foundation.AspNetCore.Features.Shared;
using Mediachase.Commerce.Orders;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.MyAccount.SubscriptionHistory.ViewModels
{
    /// <summary>
    /// Model for list all payment plans
    /// </summary>
    public class SubscriptionHistoryViewModel : ContentViewModel<SubscriptionHistoryPage>
    {
        public SubscriptionHistoryViewModel(SubscriptionHistoryPage currentPage) : base(currentPage)
        {
        }

        public List<PaymentPlan> PaymentPlans { get; set; }
        public string PaymentPlanDetailsPageUrl { get; set; }
    }
}