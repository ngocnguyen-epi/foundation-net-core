using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Commerce.Order;
using EPiServer.Core;
using EPiServer.Security;
using EPiServer.Web.Routing;
using Foundation.AspNetCore.Cms.Settings;
using Foundation.AspNetCore.Features.MyAccount.AddressBook.Models;
using Foundation.AspNetCore.Features.MyAccount.AddressBook.Services;
using Foundation.AspNetCore.Features.MyAccount.OrderHistory.ViewModels;
using Foundation.AspNetCore.Features.MyAccount.ProfilePage.ViewModels;
using Foundation.AspNetCore.Features.MyAccount.Shared.Controllers;
using Foundation.AspNetCore.Features.Settings;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Interfaces;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Models;
using Foundation.AspNetCore.Features.Shared.Commerce.Order.Interfaces;
using Foundation.AspNetCore.Infrastructure.Identity;
using Mediachase.Commerce.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.AspNetCore.Features.MyAccount.ProfilePage.Controllers
{
    [Authorize]
    public class ProfilePageController : IdentityControllerBase<Models.ProfilePage>
    {
        private readonly IAddressBookService _addressBookService;
        private readonly IOrderRepository _orderRepository;
        private readonly ICartService _cartService;
        private readonly ISettingsService _settingsService;

        public ProfilePageController(IAddressBookService addressBookService,
            IOrderRepository orderRepository,
            ApplicationSignInManager<SiteUser> signinManager,
            ApplicationUserManager<SiteUser> userManager,
            ICartService cartService,
            ICustomerService customerService,
            ISettingsService settingsService) : base(signinManager, userManager, customerService)
        {
            _addressBookService = addressBookService;
            _orderRepository = orderRepository;
            _cartService = cartService;
            _settingsService = settingsService;
        }

        public async Task<IActionResult> Index(Models.ProfilePage currentPage)
        {
            var viewModel = new ProfilePageViewModel(currentPage)
            {
                Orders = GetOrderHistoryViewModels(),
                Addresses = GetAddressViewModels(),
                SiteUser = await CustomerService.GetSiteUserAsync(User.Identity.Name),
                CustomerContact = new FoundationContact(CustomerService.GetCurrentContact().Contact),
                OrderDetailsPageUrl = UrlResolver.Current.GetUrl(_settingsService.GetSiteSettings<ReferencePageSettings>()?.OrderDetailsPage ?? ContentReference.StartPage),
                ResetPasswordPage = UrlResolver.Current.GetUrl(_settingsService.GetSiteSettings<ReferencePageSettings>()?.ResetPasswordPage ?? ContentReference.StartPage),
                AddressBookPage = UrlResolver.Current.GetUrl(_settingsService.GetSiteSettings<ReferencePageSettings>()?.AddressBookPage ?? ContentReference.StartPage)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Save(Models.ProfilePage currentPage, AccountInformationViewModel viewModel)
        {
            var user = await CustomerService.GetSiteUserAsync(User.Identity.Name);
            var contact = CustomerService.GetCurrentContact();
            user.FirstName = contact.FirstName = viewModel.FirstName;
            user.LastName = contact.LastName = viewModel.LastName;
            contact.Contact.BirthDate = viewModel.DateOfBirth;
            user.NewsLetter = viewModel.SubscribesToNewsletter;

            UserManager.UpdateAsync(user)
                .GetAwaiter()
                .GetResult();

            contact.SaveChanges();

            return Json(new { contact.FirstName, contact.LastName });
        }

        private IList<AddressModel> GetAddressViewModels() => _addressBookService.List();

        private List<OrderViewModel> GetOrderHistoryViewModels()
        {
            var purchaseOrders = _orderRepository.Load<IPurchaseOrder>(PrincipalInfo.CurrentPrincipal.GetContactId(), _cartService.DefaultCartName)
                .OrderByDescending(x => x.Created).ToList();

            if (purchaseOrders.Count > 3)
            {
                purchaseOrders = purchaseOrders.Take(3).ToList();
            }

            var viewModel = new List<OrderViewModel>();

            foreach (var purchaseOrder in purchaseOrders)
            {
                // Assume there is only one form per purchase.
                var form = purchaseOrder.GetFirstForm();
                var billingAddress = new AddressModel();
                var payment = form.Payments.FirstOrDefault();
                if (payment != null)
                {
                    billingAddress = _addressBookService.ConvertToModel(payment.BillingAddress);
                }
                var orderViewModel = new OrderViewModel
                {
                    PurchaseOrder = purchaseOrder,
                    Items = form.GetAllLineItems().Select(lineItem => new OrderHistoryItemViewModel
                    {
                        LineItem = lineItem,
                    }).GroupBy(x => x.LineItem.Code).Select(group => group.First()),
                    BillingAddress = billingAddress,
                    ShippingAddresses = new List<AddressModel>(),
                    OrderGroupId = purchaseOrder.OrderLink.OrderGroupId
                };

                foreach (var orderAddress in purchaseOrder.Forms.SelectMany(x => x.Shipments).Select(s => s.ShippingAddress))
                {
                    var shippingAddress = _addressBookService.ConvertToModel(orderAddress);
                    orderViewModel.ShippingAddresses.Add(shippingAddress);
                }

                viewModel.Add(orderViewModel);
            }

            return viewModel;
        }
    }
}