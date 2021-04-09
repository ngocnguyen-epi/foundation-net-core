using EPiServer;
using EPiServer.Core;
using EPiServer.Personalization;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Linq;

namespace Foundation.AspNetCore.Features.CmsPages.Location.LocationListPage
{
    public class LocationListPageController : PageController<Models.LocationListPage>
    {
        private readonly IContentLoader _contentLoader;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocationListPageController(IContentLoader contentLoader,
            IHttpContextAccessor httpContextAccessor)
        {
            _contentLoader = contentLoader;
            _httpContextAccessor = httpContextAccessor;
        }

        public ActionResult Index(Models.LocationListPage currentPage)
        {
            // Filter: Not support yet
            //var query = SearchClient.Instance.Search<LocationItemPage.LocationItemPage>()
            //    .PublishedInCurrentLanguage()
            //    .FilterOnReadAccess();

            //if (currentPage.FilterArea != null)
            //{
            //    foreach (var filterBlock in currentPage.FilterArea.FilteredItems)
            //    {
            //        var b = _contentLoader.Get<BlockData>(filterBlock.ContentLink) as IFilterBlock;
            //        if (b != null)
            //        {
            //            query = b.AddFilter(query);
            //        }
            //    }

            //    foreach (var filterBlock in currentPage.FilterArea.FilteredItems)
            //    {
            //        var b = _contentLoader.Get<BlockData>(filterBlock.ContentLink) as IFilterBlock;
            //        if (b != null)
            //        {
            //            query = b.ApplyFilter(query, Request.QueryString);
            //        }
            //    }
            //}

            //var locations = query.OrderBy(x => x.PageName)
            //                        .Take(500)
            //                        .StaticallyCacheFor(new System.TimeSpan(0, 1, 0)).GetContentResult();

            var contentReferences = _contentLoader.GetDescendents(ContentReference.StartPage);
            var locations = contentReferences.Select(x => _contentLoader.Get<IContent>(x))
                .OfType<LocationItemPage.Models.LocationItemPage>()
                .OrderBy(x => x.Name)
                .Take(500);

            var model = new LocationListViewModel(currentPage)
            {
                Locations = locations,
                MapCenter = GetMapCenter(),
                //UserLocation = GetMapCenter(),
                QueryString = _httpContextAccessor.HttpContext.Request.Query
            };

            return View(model);
        }

        private static GeoCoordinate GetMapCenter()
        {
            //var userLocation = GeoPosition.GetUsersPosition();
            //if (userLocation != null)
            //{
            //    return new GeoCoordinate(30, userLocation.Longitude);
            //}
            return new GeoCoordinate(30, 0);
        }
    }
}