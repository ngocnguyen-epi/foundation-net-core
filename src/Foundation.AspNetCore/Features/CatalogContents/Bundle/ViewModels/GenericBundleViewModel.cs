using Foundation.AspNetCore.Features.CatalogContents.Bundle.Models;

namespace Foundation.AspNetCore.Features.CatalogContents.Bundle.ViewModels
{
    public class GenericBundleViewModel : BundleViewModelBase<GenericBundle>
    {
        public GenericBundleViewModel()
        {
        }

        public GenericBundleViewModel(GenericBundle fashionBundle) : base(fashionBundle)
        {
        }
    }
}
