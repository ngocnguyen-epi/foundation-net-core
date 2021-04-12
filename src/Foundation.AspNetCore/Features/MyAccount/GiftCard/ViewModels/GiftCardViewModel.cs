using Foundation.AspNetCore.Features.MyAccount.GiftCard.Models;
using Foundation.AspNetCore.Features.Shared;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.MyAccount.GiftCard.ViewModels
{
    public class GiftCardViewModel : ContentViewModel<GiftCardPage>
    {
        public GiftCardViewModel(GiftCardPage currentPage) : base(currentPage)
        {
        }

        public List<GiftCardModel> GiftCardList { get; set; }
    }
}