using EPiServer.Core;
using Foundation.AspNetCore.Features.Shared;
using Foundation.AspNetCore.Features.Shared.Commerce.Order.ViewModels;
using Mediachase.Commerce;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.NamedCarts.DefaultCart.ViewModels
{
    public abstract class CartViewModelBase<T> : ContentViewModel<T> where T : IContent
    {
        protected CartViewModelBase(T content) : base(content)
        {
        }

        public decimal ItemCount { get; set; }

        public IEnumerable<CartItemViewModel> CartItems { get; set; }

        public Money Total { get; set; }

        public bool HasOrganization { get; set; }
    }
}