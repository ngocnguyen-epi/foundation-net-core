using EPiServer.Core;
using EPiServer.Security;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using Foundation.AspNetCore.Cms.Settings;
using Foundation.AspNetCore.Features.MyAccount.SubscriptionHistory.Models;
using Foundation.AspNetCore.Features.MyAccount.SubscriptionHistory.ViewModels;
using Foundation.AspNetCore.Features.Settings;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Security;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Foundation.AspNetCore.Features.MyAccount.SubscriptionHistory.Controllers
{
    public class SubscriptionHistoryController : PageController<SubscriptionHistoryPage>
    {
        private readonly ISettingsService _settingsService;

        public SubscriptionHistoryController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public IActionResult Index(SubscriptionHistoryPage currentPage)
        {
            var paymentPlans = OrderContext.Current.LoadByCustomerId<PaymentPlan>(PrincipalInfo.CurrentPrincipal.GetContactId())
                .OrderBy(x => x.Created)
                .ToList();
            
            var viewModel = new SubscriptionHistoryViewModel(currentPage)
            {
                CurrentContent = currentPage,
                PaymentPlans = paymentPlans
            };

            viewModel.PaymentPlanDetailsPageUrl = UrlResolver.Current.GetUrl(_settingsService.GetSiteSettings<ReferencePageSettings>()?.PaymentPlanDetailsPage ?? ContentReference.StartPage);

            return View(viewModel);
        }
    }
}