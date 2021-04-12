using Foundation.AspNetCore.Features.MyAccount.CreditCard.Models;
using Foundation.AspNetCore.Features.Shared;
using Foundation.AspNetCore.Features.Shared.Commerce.Customer.Models;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.MyAccount.CreditCard.ViewModels
{
    /// <summary>
    /// Represent for all credit cards of user or an organization
    /// </summary>
    public class CreditCardCollectionViewModel : ContentViewModel<CreditCardPage>
    {
        public CreditCardCollectionViewModel(CreditCardPage currentPage) : base(currentPage)
        {
        }

        public IEnumerable<CreditCardModel> CreditCards { get; set; }
        public FoundationContact CurrentContact { get; set; }
        public bool IsB2B { get; set; }
    }
}