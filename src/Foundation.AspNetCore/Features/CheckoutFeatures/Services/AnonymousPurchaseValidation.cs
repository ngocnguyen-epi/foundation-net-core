using EPiServer.Framework.Localization;
using Foundation.AspNetCore.Features.CheckoutFeatures.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Foundation.AspNetCore.Features.CheckoutFeatures.Services
{
    public class AnonymousPurchaseValidation : PurchaseValidation
    {
        public AnonymousPurchaseValidation(LocalizationService localizationService) : base(localizationService)
        {
        }

        public override bool ValidateModel(ModelStateDictionary modelState, CheckoutViewModel viewModel)
        {
            return
                ValidateBillingAddress(modelState, viewModel) &&
                ValidateShippingMethods(modelState, viewModel);
        }

        private bool ValidateBillingAddress(ModelStateDictionary modelState, CheckoutViewModel viewModel)
        {
            if (viewModel.UseShippingingAddressForBilling)
            {
                foreach (var state in modelState.Where(x => x.Key.StartsWith("Shipments")).ToArray())
                {
                    modelState.Remove(state.Key);
                }
            }

            if (string.IsNullOrEmpty(viewModel.BillingAddress.Email))
            {
                modelState.AddModelError("BillingAddress.Email", LocalizationService.GetString("/Shared/Address/Form/Empty/Email"));
            }

            return modelState.IsValid;
        }
    }
}