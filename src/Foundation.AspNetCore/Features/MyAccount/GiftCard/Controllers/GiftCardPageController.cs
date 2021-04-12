using EPiServer.Web.Mvc;
using Foundation.AspNetCore.Features.MyAccount.GiftCard.Interfaces;
using Foundation.AspNetCore.Features.MyAccount.GiftCard.Models;
using Foundation.AspNetCore.Features.MyAccount.GiftCard.ViewModels;
using Mediachase.Commerce.Customers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Foundation.AspNetCore.Features.MyAccount.GiftCard.Controllers
{
    /// <summary>
    /// A page to list all gift card belonging to a customer
    /// </summary>
    public class GiftCardPageController : PageController<GiftCardPage>
    {
        private readonly IGiftCardService _giftCardService;

        public GiftCardPageController(IGiftCardService giftCardService)
        {
            _giftCardService = giftCardService;
        }

        public IActionResult Index(GiftCardPage currentPage)
        {
            var model = new GiftCardViewModel(currentPage)
            {
                CurrentContent = currentPage,
                GiftCardList = _giftCardService.GetCustomerGiftCards(CustomerContext.Current.CurrentContactId.ToString()).ToList()
            };

            return View(model);
        }
    }
}