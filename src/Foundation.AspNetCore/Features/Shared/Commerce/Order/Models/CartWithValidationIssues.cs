using EPiServer.Commerce.Order;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.Shared.Commerce.Order.Models
{
    public class CartWithValidationIssues
    {
        public virtual ICart Cart { get; set; }
        public virtual Dictionary<ILineItem, List<ValidationIssue>> ValidationIssues { get; set; }
    }
}