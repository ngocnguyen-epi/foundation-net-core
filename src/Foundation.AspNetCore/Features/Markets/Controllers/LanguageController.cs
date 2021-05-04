using EPiServer.Core;
using EPiServer.Web.Routing;
using Foundation.AspNetCore.Cms.Extensions;
using Foundation.AspNetCore.Features.Shared.Commerce.Market.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.AspNetCore.Features.Markets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LanguageController : ControllerBase
    {
        private readonly LanguageService _languageService;
        private readonly UrlResolver _urlResolver;
        private readonly IContentRouteHelper _contentRouteHelper;

        public LanguageController(LanguageService languageService, UrlResolver urlResolver, IContentRouteHelper contentRouteHelper)

        {
            _languageService = languageService;
            _urlResolver = urlResolver;
            _contentRouteHelper = contentRouteHelper;
        }

        [HttpPost]
        [Route("Set")]
        public ActionResult Set([FromForm]string language, ContentReference contentLink)
        {
            _languageService.SetRoutedContent(_contentRouteHelper.Content, language);

            var returnUrl = _urlResolver.GetUrl(Request, contentLink, language);
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new { returnUrl }),
                ContentType = "application/json",
            };
        }
    }
}
