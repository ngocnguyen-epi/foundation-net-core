using EPiServer.Personalization.Commerce.Tracking;
using Foundation.AspNetCore.Features.CatalogContents.Product.Models;
using Foundation.AspNetCore.Features.CatalogContents.Shared.ViewModels;
using Foundation.AspNetCore.Features.CatalogContents.Variation.Models;
using Foundation.AspNetCore.Features.Shared.ViewModels;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.CatalogContents.Product.ViewModels
{
    public class GenericProductViewModel : ProductViewModelBase<GenericProduct, GenericVariant>, IEntryViewModelBase
    {
        public GenericProductViewModel()
        {
        }

        public GenericProductViewModel(GenericProduct fashionProduct) : base(fashionProduct)
        {
        }

        public ReviewsViewModel Reviews { get; set; }
        public IEnumerable<Recommendation> AlternativeProducts { get; set; }
        public IEnumerable<Recommendation> CrossSellProducts { get; set; }
    }
}