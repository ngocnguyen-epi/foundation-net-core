using EPiServer.Personalization;
using Foundation.AspNetCore.Features.Shared;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Foundation.AspNetCore.Features.CmsPages.Location.LocationListPage
{
    public class LocationListViewModel : ContentViewModel<Models.LocationListPage>
    {
        public LocationListViewModel(Models.LocationListPage currentPage) : base(currentPage)
        {
        }

        public GeoCoordinate MapCenter { get; set; }
        //public IGeolocationResult UserLocation { get; set; }
        public IEnumerable<LocationItemPage.Models.LocationItemPage> Locations { get; set; }
        public IQueryCollection QueryString { get; set; }
    }
}