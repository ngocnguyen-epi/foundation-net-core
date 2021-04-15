using Foundation.AspNetCore.Features.Shared;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.MyOrganization.QuickOrderPage.ViewModels
{
    public class QuickOrderPageViewModel : ContentViewModel<Models.QuickOrderPage>
    {
        public List<ProductTileViewModel> ProductsList { get; set; }
        public List<string> ReturnedMessages { get; set; }
    }
}