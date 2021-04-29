using EPiServer.Commerce.Catalog.ContentTypes;
using Foundation.AspNetCore.Features.CatalogContents.Shared.ViewModels;
using System.Collections.Generic;

namespace Foundation.Features.CatalogContent.Variation
{
    public abstract class VariantViewModelBase<TVariant> : EntryViewModelBase<TVariant> where TVariant : VariationContent
    {
        protected VariantViewModelBase()
        {
        }

        protected VariantViewModelBase(TVariant genericVariant) : base(genericVariant)
        {
            Variant = genericVariant;
        }

        public TVariant Variant { get; set; }
        public IEnumerable<EntryContentBase> Entries { get; set; }
    }
}
