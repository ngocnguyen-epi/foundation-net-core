using EPiServer.Commerce.Order;
using EPiServer.ServiceLocation;
using Foundation.AspNetCore.Features.Shared.Commerce.Payment.PaymentOpstions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Features.Shared.Commerce.Payment
{
    public class PaymentModelBinderProvider : IModelBinderProvider
    {
        private static readonly IDictionary<Type, Type> ModelBinderTypeMappings = new Dictionary<Type, Type>
        {
            {typeof(IPaymentMethod), typeof(PaymentOptionViewModelBinder)},
        };

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            var modelType = context.BindingInfo.BinderType;
            if (ModelBinderTypeMappings.ContainsKey(modelType))
            {
                return ServiceLocator.Current.GetService(ModelBinderTypeMappings[modelType]) as IModelBinder;
            }

            return null;
        }
    }
}
