using EPiServer.Commerce.Order;
using Foundation.AspNetCore.Features.Shared.Commerce.Market.Interfaces;
using Foundation.AspNetCore.Features.Shared.Commerce.Order.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Foundation.AspNetCore.Features.Markets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        //private readonly ICartService _cartService;
        private readonly IOrderRepository _orderRepository;

        public CurrencyController(ICurrencyService currencyService,
            //ICartService cartService,
            IOrderRepository orderRepository)
        {
            _currencyService = currencyService;
            //_cartService = cartService;
            _orderRepository = orderRepository;
        }

        [HttpPost]
        [Route("Set")]
        public ActionResult Set([FromForm] string currencyCode)
        {
            if (!_currencyService.SetCurrentCurrency(currencyCode))
            {
                return new BadRequestResult();
            }

            //var cart = _cartService.LoadCart(_cartService.DefaultCartName, true)?.Cart;
            //if (cart != null)
            //{
            //    var currentCurrency = new Mediachase.Commerce.Currency(currencyCode);
            //    if (currentCurrency != cart.Currency)
            //    {
            //        _cartService.SetCartCurrency(cart, currentCurrency);
            //        _orderRepository.Save(cart);
            //    }
            //}
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new { returnUrl = !string.IsNullOrEmpty(Request.Headers["Referer"]) ? Request.Headers["Referer"].ToString() : "/" }),
                ContentType = "application/json",
            };
        }
    }
}
