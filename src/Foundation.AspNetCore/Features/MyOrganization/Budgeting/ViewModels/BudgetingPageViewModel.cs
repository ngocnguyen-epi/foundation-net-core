using Foundation.AspNetCore.Features.MyOrganization.Budgeting.Models;
using Foundation.AspNetCore.Features.Shared;
using Foundation.AspNetCore.Features.Shared.Commerce.Organization.ViewModels;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.MyOrganization.Budgeting.ViewModels
{
    public class BudgetingPageViewModel : ContentViewModel<BudgetingPage>
    {
        public List<BudgetViewModel> OrganizationBudgets { get; set; }
        public List<BudgetViewModel> SubOrganizationsBudgets { get; set; }
        public List<BudgetViewModel> PurchasersSpendingLimits { get; set; }
        public BudgetViewModel NewBudgetOption { get; set; }
        public List<string> AvailableCurrencies { get; set; }
        public BudgetViewModel CurrentBudgetViewModel { get; set; }
        public bool IsSubOrganization { get; set; }
        public bool IsAdmin { get; set; }
    }
}