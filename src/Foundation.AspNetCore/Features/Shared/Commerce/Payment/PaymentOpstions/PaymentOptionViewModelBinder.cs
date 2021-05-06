using Foundation.AspNetCore.Features.Shared.Commerce.Payment.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Threading.Tasks;

namespace Foundation.AspNetCore.Features.Shared.Commerce.Payment.PaymentOpstions
{
    public class PaymentOptionViewModelBinder : IModelBinder
    {
        private readonly PaymentMethodViewModelFactory _paymentMethodViewModelFactory;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public PaymentOptionViewModelBinder(PaymentMethodViewModelFactory paymentMethodViewModelFactory,
            IModelMetadataProvider modelMetadataProvider)
        {
            _paymentMethodViewModelFactory = paymentMethodViewModelFactory;
            _modelMetadataProvider = modelMetadataProvider;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var systemKeyword = bindingContext.ValueProvider.GetValue("SystemKeyword").FirstValue;
            if (string.IsNullOrEmpty(systemKeyword))
            {
                return Task.CompletedTask;
            }

            var paymentMethodViewModels = _paymentMethodViewModelFactory.GetPaymentMethodViewModels();
            var selectedPaymentMethod = paymentMethodViewModels.FirstOrDefault(p => p.SystemKeyword == systemKeyword);

            bindingContext.ModelMetadata = _modelMetadataProvider.GetMetadataForType(selectedPaymentMethod?.PaymentOption.GetType());
            return Task.CompletedTask;
        }
    }
}
