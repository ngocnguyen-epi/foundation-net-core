using Mediachase.Commerce;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.Shared.Commerce.Market.Interfaces
{
    public interface ICurrencyService
    {
        IEnumerable<Currency> GetAvailableCurrencies();
        Currency GetCurrentCurrency();
        bool SetCurrentCurrency(string currencyCode);
    }
}