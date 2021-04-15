using EPiServer.Core;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using Foundation.AspNetCore.Cms.Settings;
using Foundation.AspNetCore.Features.MyOrganization.Orders.Models;
using Foundation.AspNetCore.Features.MyOrganization.Orders.ViewModels;
using Foundation.AspNetCore.Features.Settings;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Interfaces;
using Foundation.AspNetCore.Features.Shared.Commerce.Order.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.MyOrganization.Orders.Controllers
{
    [Authorize]
    public class OrdersController : PageController<OrdersPage>
    {
        private readonly ICustomerService _customerService;
        private readonly IOrdersService _ordersService;
        private readonly ISettingsService _settingsService;

        public OrdersController(ICustomerService customerService,
            IOrdersService ordersService,
            ISettingsService settingsService)
        {
            _customerService = customerService;
            _ordersService = ordersService;
            _settingsService = settingsService;
        }

        public IActionResult Index(OrdersPage currentPage)
        {
            var organizationUsersList = _customerService.GetContactsForOrganization();
            var viewModel = new OrdersPageViewModel
            {
                CurrentContent = currentPage
            };

            var ordersOrganization = new List<OrderOrganizationViewModel>();
            foreach (var user in organizationUsersList)
            {
                ordersOrganization.AddRange(_ordersService.GetUserOrders(user.ContactId));
            }
            viewModel.OrdersOrganization = ordersOrganization;

            viewModel.OrderDetailsPageUrl =
                UrlResolver.Current.GetUrl(_settingsService.GetSiteSettings<ReferencePageSettings>()?.OrderDetailsPage ?? ContentReference.StartPage);
            return View(viewModel);
        }

        public IActionResult QuickOrder(OrdersPage currentPage)
        {
            var viewModel = new OrdersPageViewModel { CurrentContent = currentPage };
            return View(viewModel);
        }
    }
}