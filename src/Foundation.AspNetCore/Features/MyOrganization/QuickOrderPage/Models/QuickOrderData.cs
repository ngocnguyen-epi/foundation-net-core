using FileHelpers;

namespace Foundation.AspNetCore.Features.MyOrganization.QuickOrderPage.Models
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines]
    public class QuickOrderData
    {
        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Sku;

        [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public int Quantity;
    }
}