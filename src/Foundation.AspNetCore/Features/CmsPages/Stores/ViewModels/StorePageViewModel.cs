using Foundation.AspNetCore.Features.CmsPages.Stores.Models;
using Foundation.AspNetCore.Features.Shared;

namespace Foundation.AspNetCore.Features.CmsPages.Stores.ViewModels
{
    public class StorePageViewModel : ContentViewModel<StorePage>
    {
        public StoreViewModel StoreViewModel { get; set; }

        public StorePageViewModel(StorePage currentPage) : base(currentPage) { }
    }
}