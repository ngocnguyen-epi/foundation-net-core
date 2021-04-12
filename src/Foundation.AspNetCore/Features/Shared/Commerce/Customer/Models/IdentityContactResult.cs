using Microsoft.AspNetCore.Identity;

namespace Foundation.AspNetCore.Features.Shared.Commerce.Customer.Models
{
    public class IdentityContactResult
    {
        public IdentityResult IdentityResult { get; set; }
        public FoundationContact FoundationContact { get; set; }
        public IdentityContactResult() => IdentityResult = new IdentityResult();
    }
}
