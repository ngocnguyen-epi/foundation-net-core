using EPiServer.Core;
using Foundation.AspNetCore.Features.NamedCarts.Wishlist.Models;
using Foundation.AspNetCore.Features.Shared.Commerce.Order.Models;

namespace Foundation.AspNetCore.Features.Shared.Commerce.Order.ViewModels
{
    public class WishListMiniCartViewModel : CartViewModelBase<WishListPage>
    {
        public WishListMiniCartViewModel(WishListPage wishListPage) : base(wishListPage)
        {
        }

        public ContentReference WishListPage { get; set; }

        public string Label { get; set; }
    }
}