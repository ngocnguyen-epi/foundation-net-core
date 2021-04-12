using Foundation.AspNetCore.Features.MyAccount.GiftCard.Models;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.MyAccount.GiftCard.Interfaces
{
    public interface IGiftCardService
    {
        List<GiftCardModel> GetAllGiftCards();
        List<GiftCardModel> GetCustomerGiftCards(string contactId);
        string CreateGiftCard(GiftCardModel giftCard);
        string UpdateGiftCard(GiftCardModel giftCard);
        string DeleteGiftCard(string giftCardId);
        GiftCardModel GetGiftCard(string giftCardId);
    }
}
