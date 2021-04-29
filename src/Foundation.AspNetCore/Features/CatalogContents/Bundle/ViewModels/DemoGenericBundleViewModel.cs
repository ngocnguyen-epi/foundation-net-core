using EPiServer.Personalization.Commerce.Tracking;
using Foundation.AspNetCore.Features.CatalogContents.Shared.ViewModels;
using Foundation.AspNetCore.Features.Shared.ViewModels;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.CatalogContents.Bundle.ViewModels
{
    public class DemoGenericBundleViewModel : GenericBundleViewModel, IEntryViewModelBase
    {
        public ReviewsViewModel Reviews { get; set; }
        public IEnumerable<Recommendation> AlternativeProducts { get; set; }
        public IEnumerable<Recommendation> CrossSellProducts { get; set; }
    }
}
