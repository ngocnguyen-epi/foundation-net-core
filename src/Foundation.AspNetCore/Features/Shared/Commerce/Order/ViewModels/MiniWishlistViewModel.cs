using EPiServer.Core;
using Mediachase.Commerce;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.Shared.Commerce.Order.ViewModels
{
    public class MiniWishlistViewModel
    {
        public MiniWishlistViewModel()
        {
            Items = new List<CartItemViewModel>();
        }

        public ContentReference WishlistPage { get; set; }

        public decimal ItemCount { get; set; }

        public IEnumerable<CartItemViewModel> Items { get; set; }

        public Money Total { get; set; }

        public string Label { get; set; }

        public bool HasOrganization { get; set; }
    }
}