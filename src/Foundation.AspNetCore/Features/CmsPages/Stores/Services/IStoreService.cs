using Foundation.AspNetCore.Features.CmsPages.Stores.ViewModels;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.CmsPages.Stores.Services
{
    public interface IStoreService
    {
        List<StoreItemViewModel> GetEntryStoresViewModels(string entryCode);
        List<StoreItemViewModel> GetAllStoreViewModels();
        StoreItemViewModel GetCurrentStoreViewModel();
        bool SetCurrentStore(string storeCode);
    }
}
