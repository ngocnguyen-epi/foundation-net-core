using Foundation.AspNetCore.Features.CatalogContents.Shared.ViewModels;
using Foundation.AspNetCore.Features.CatalogContents.Variation.Models;

namespace Foundation.AspNetCore.Features.CatalogContents.Variation.ViewModels
{
    public class GenericVariantViewModel : EntryViewModelBase<GenericVariant>
    {
        public GenericVariantViewModel()
        {
        }

        public GenericVariantViewModel(GenericVariant variantBase) : base(variantBase)
        {
        }
    }
}
