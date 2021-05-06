using Foundation.AspNetCore.Features.NamedCarts.DefaultCart.ViewModels;
using Foundation.AspNetCore.Features.NamedCarts.Wishlist.Models;

namespace Foundation.AspNetCore.Features.NamedCarts.Wishlist.ViewModels
{
    public class WishListViewModel : CartViewModelBase<WishListPage>
    {
        public WishListViewModel(WishListPage wishListPage) : base(wishListPage)
        {
        }
    }
}