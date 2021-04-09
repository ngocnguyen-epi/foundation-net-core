using EPiServer.Core;
using Foundation.AspNetCore.Features.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.AspNetCore.Features.CmsPages.Location.LocationItemPage
{
    public class LocationItemViewModel : ContentViewModel<Models.LocationItemPage>
    {
        public LocationItemViewModel(Models.LocationItemPage currentPage) : base(currentPage)
        {
            LocationNavigation = new LocationNavigationModel
            {
                CurrentLocation = currentPage
            };
        }

        public ImageData Image { get; set; }
        public LocationNavigationModel LocationNavigation { get; set; }
        public IEnumerable<Models.LocationItemPage> SimilarLocations { get; set; }
        //public IEnumerable<StandardCategory> Tags { get; set; }
    }

    public class LocationNavigationModel
    {
        public LocationNavigationModel()
        {
            CloseBy = Enumerable.Empty<Models.LocationItemPage>();
            ContinentLocations = Enumerable.Empty<Models.LocationItemPage>();
        }

        public IEnumerable<Models.LocationItemPage> CloseBy { get; set; }
        public IEnumerable<Models.LocationItemPage> ContinentLocations { get; set; }
        public Models.LocationItemPage CurrentLocation { get; set; }
    }
}