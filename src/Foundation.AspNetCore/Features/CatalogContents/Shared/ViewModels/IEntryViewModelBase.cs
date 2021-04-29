using EPiServer.Personalization.Commerce.Tracking;
using Foundation.AspNetCore.Features.Shared.ViewModels;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.CatalogContents.Shared.ViewModels
{
    public interface IEntryViewModelBase
    {
        ReviewsViewModel Reviews { get; set; }
        IEnumerable<Recommendation> AlternativeProducts { get; set; }
        IEnumerable<Recommendation> CrossSellProducts { get; set; }
    }
}