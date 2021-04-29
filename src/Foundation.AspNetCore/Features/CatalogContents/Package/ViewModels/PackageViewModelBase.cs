using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using Foundation.AspNetCore.Features.CatalogContents.Shared.ViewModels;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.CatalogContents.Package.ViewModels
{
    public abstract class PackageViewModelBase<TPackage> : EntryViewModelBase<TPackage> where TPackage : PackageContent
    {
        protected PackageViewModelBase()
        {
        }

        protected PackageViewModelBase(TPackage package) : base(package)
        {
            Package = package;
        }

        public TPackage Package { get; set; }
        public IEnumerable<CatalogContentBase> Entries { get; set; }
        public IEnumerable<EntryRelation> EntriesRelation { get; set; }
    }
}
