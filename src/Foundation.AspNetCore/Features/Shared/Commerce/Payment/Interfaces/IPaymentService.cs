using Foundation.AspNetCore.Features.Shared.Commerce.Payment.ViewModels;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.Shared.Commerce.Payment.Interfaces
{
    public interface IPaymentService
    {
        IEnumerable<PaymentMethodViewModel> GetPaymentMethodsByMarketIdAndLanguageCode(string marketId, string languageCode);
    }
}