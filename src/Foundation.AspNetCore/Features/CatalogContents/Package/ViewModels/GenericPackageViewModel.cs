using EPiServer.Personalization.Commerce.Tracking;
using Foundation.AspNetCore.Features.CatalogContents.Package.Models;
using Foundation.AspNetCore.Features.CatalogContents.Shared.ViewModels;
using Foundation.AspNetCore.Features.Shared.ViewModels;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.CatalogContents.Package.ViewModels
{
    public class GenericPackageViewModel : PackageViewModelBase<GenericPackage>, IEntryViewModelBase
    {
        public GenericPackageViewModel()
        {
        }

        public GenericPackageViewModel(GenericPackage fashionPackage) : base(fashionPackage)
        {
        }

        public ReviewsViewModel Reviews { get; set; }
        public IEnumerable<Recommendation> AlternativeProducts { get; set; }
        public IEnumerable<Recommendation> CrossSellProducts { get; set; }
    }
}
