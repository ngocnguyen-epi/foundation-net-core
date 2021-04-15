using EPiServer;
using EPiServer.Commerce.Order;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using EPiServer.Web.Mvc.Html;
using Foundation.AspNetCore.Cms.Settings;
using Foundation.AspNetCore.Features.MyOrganization.QuickOrderPage.Models;
using Foundation.AspNetCore.Features.Settings;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Interfaces;
using Foundation.AspNetCore.Features.Shared.Commerce.Order.Interfaces;
using Foundation.AspNetCore.Features.Shared.Commerce.Order.Models;
using Foundation.AspNetCore.Features.Shared.Interfaces;
using Foundation.AspNetCore.Infrastructure;
using Mediachase.Commerce.Catalog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.AspNetCore.Features.MyOrganization.QuickOrderBlock
{
    [TemplateDescriptor(Inherited = true)]
    public class QuickOrderBlockComponent : BlockComponent<QuickOrderBlock>
    {
        private ICart _cart;
        private readonly IQuickOrderService _quickOrderService;
        private readonly ICartService _cartService;
        private readonly IFileHelperService _fileHelperService;
        private readonly IOrderRepository _orderRepository;
        private readonly ReferenceConverter _referenceConverter;
        private readonly ISearchService _searchService;
        private readonly ICustomerService _customerService;
        private readonly IContentLoader _contentLoader;
        private readonly ISettingsService _settingsService;

        public QuickOrderBlockComponent(
            IQuickOrderService quickOrderService,
            ICartService cartService,
            IFileHelperService fileHelperService,
            IOrderRepository orderRepository,
            ReferenceConverter referenceConverter,
            ISearchService searchService,
            ICustomerService customerService,
            IContentLoader contentLoader,
            ISettingsService settingsService)
        {
            _quickOrderService = quickOrderService;
            _cartService = cartService;
            _fileHelperService = fileHelperService;
            _orderRepository = orderRepository;
            _referenceConverter = referenceConverter;
            _searchService = searchService;
            _customerService = customerService;
            _contentLoader = contentLoader;
            _settingsService = settingsService;
        }
        public IViewComponentResult Index(QuickOrderBlock currentBlock)
        {
            var model = new QuickOrderViewModel(currentBlock);

            model.ReturnedMessages = TempData["messages"] as List<string>;
            model.ProductsList = TempData["products"] as List<QuickOrderProductViewModel>;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IViewComponentResult Import(QuickOrderProductViewModel[] productsList)
        {
            var returnedMessages = new List<string>();

            ModelState.Clear();

            if (Cart == null)
            {
                _cart = _cartService.LoadOrCreateCart(_cartService.DefaultCartName);
            }

            foreach (var product in productsList)
            {
                if (!product.ProductName.Equals("removed"))
                {
                    var variationReference = _referenceConverter.GetContentLink(product.Sku);
                    var currentQuantity = GetCurrentItemQuantity(product.Sku);
                    product.Quantity += (int)currentQuantity;
                    var responseMessage = _quickOrderService.ValidateProduct(variationReference, Convert.ToDecimal(product.Quantity), product.Sku);
                    AddToCartQuickOrder(_cart, product, returnedMessages, responseMessage);
                }
            }

            if (returnedMessages.Count == 0)
            {
                returnedMessages.Add("All items were added to cart.");
            }
            TempData["messages"] = returnedMessages;

            var model = new { Message = returnedMessages, TotalItem = Cart.GetAllLineItems().Sum(x => x.Quantity) };
            return new ContentViewComponentResult(JsonConvert.SerializeObject(model));
        }

        private decimal GetCurrentItemQuantity(string variantCode)
        {
            if (Cart == null)
            {
                return 0;
            }

            var lineItem = Cart.GetAllLineItems().Where(x => x.Code == variantCode).FirstOrDefault();
            if (lineItem != null)
            {
                return lineItem.Quantity;
            }

            return 0;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IViewComponentResult AddFromFile(IFormFile file)
        {
            var fileContent = file;
            var stringResult = "";
            if (fileContent != null && fileContent.Length > 0)
            {
                var uploadedFile = fileContent.OpenReadStream();
                var fileName = fileContent.FileName;
                var productsList = new List<QuickOrderProductViewModel>();

                //validation for csv
                if (!fileName.Contains(".csv"))
                {
                    TempData["messages"] = new List<string>() { "The uploaded file is not valid!" };
                    stringResult = JsonConvert.SerializeObject(new { Message = TempData["messages"] });
                }

                var fileData = _fileHelperService.GetImportData<QuickOrderData>(uploadedFile);
                foreach (var record in fileData)
                {
                    //find the product
                    var variationReference = _referenceConverter.GetContentLink(record.Sku);
                    var product = _quickOrderService.GetProductByCode(variationReference);

                    product.Quantity = record.Quantity;
                    product.TotalPrice = product.Quantity * product.UnitPrice;

                    productsList.Add(product);
                }

                stringResult = JsonConvert.SerializeObject(new { Status = "OK", Message = "Import .csv file successfully", Products = productsList });
            }
            else
            {
                stringResult = JsonConvert.SerializeObject(new { Message = "The uploaded file is not valid!" });
            }

            return new ContentViewComponentResult(stringResult);
        }

        public IViewComponentResult GetSku(string query)
        {
            var data = _searchService.SearchSkus(query);
            return new ContentViewComponentResult(JsonConvert.SerializeObject(data));
        }

        [HttpPost]

        public IActionResult RequestQuote(QuickOrderProductViewModel[] productsList)
        {
            var returnedMessages = new List<string>();
            ModelState.Clear();

            var referencePages = _settingsService.GetSiteSettings<ReferencePageSettings>();
            var quoteCart = _cartService.LoadOrCreateCart("QuickOrderToQuote");

            if (quoteCart != null)
            {
                foreach (var product in productsList)
                {
                    if (!product.ProductName.Equals("removed"))
                    {
                        var variationReference = _referenceConverter.GetContentLink(product.Sku);
                        var responseMessage = _quickOrderService.ValidateProduct(variationReference, Convert.ToDecimal(product.Quantity), product.Sku);

                        AddToCartQuickOrder(quoteCart, product, returnedMessages, responseMessage);
                    }
                }

                _cartService.PlaceCartForQuote(quoteCart);
                _cartService.DeleteCart(quoteCart);

                var quickOrderPage = GetQuickOrderPage();
                return new RedirectResult(quickOrderPage?.LinkURL ?? Request.Headers["Referer"].ToString());
            }

            var redirectPageReference = referencePages?.OrderHistoryPage ?? ContentReference.StartPage;
            return new RedirectResult(Url.ContentUrl(redirectPageReference));
        }
        private ICart Cart => _cart ?? (_cart = _cartService.LoadCart(_cartService.DefaultCartName, true)?.Cart);

        private QuickOrderPage.Models.QuickOrderPage GetQuickOrderPage() => _contentLoader.FindPagesRecursively<QuickOrderPage.Models.QuickOrderPage>(ContentReference.StartPage).FirstOrDefault();


        private void AddToCartQuickOrder(ICart cart, QuickOrderProductViewModel product, List<string> returnedMessages, string responseMessage)
        {
            if (string.IsNullOrEmpty(responseMessage))
            {
                var result = _cartService.AddToCart(cart, new RequestParamsToCart { Code = product.Sku, Quantity = 1, Store = "delivery", SelectedStore = "" });
                if (result.EntriesAddedToCart)
                {
                    _cartService.ChangeCartItem(cart, 0, product.Sku, product.Quantity, "", "");
                    _orderRepository.Save(cart);
                }
            }
            else
            {
                returnedMessages.Add(responseMessage);
            }
        }
    }
}
