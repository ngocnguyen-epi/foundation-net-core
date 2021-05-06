using Foundation.AspNetCore.Features.NamedCarts.DefaultCart.ViewModels;
using Foundation.AspNetCore.Features.NamedCarts.SharedCart.Models;

namespace Foundation.AspNetCore.Features.NamedCarts.SharedCart.ViewModels
{
    public class SharedCartViewModel : CartViewModelBase<SharedCartPage>
    {
        public SharedCartViewModel(SharedCartPage sharedCartPage) : base(sharedCartPage)
        {
        }
    }
}