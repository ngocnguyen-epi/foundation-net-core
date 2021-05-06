using EPiServer.Core;
using Foundation.AspNetCore.Features.NamedCarts.SharedCart.Models;
using Foundation.AspNetCore.Features.Shared.Commerce.Order.Models;

namespace Foundation.AspNetCore.Features.NamedCarts.SharedCart.ViewModels
{
    public class SharedMiniCartViewModel : CartViewModelBase<SharedCartPage>
    {
        public SharedMiniCartViewModel(SharedCartPage sharedCartPage) : base(sharedCartPage)
        {
        }

        public ContentReference SharedCartPage { get; set; }
    }
}