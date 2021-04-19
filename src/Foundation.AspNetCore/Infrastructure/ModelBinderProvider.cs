using Foundation.AspNetCore.Features.Search.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;

namespace Foundation.AspNetCore.Infrastructure
{
    public class ModelBinderProvider : IModelBinderProvider
    {
        private static readonly IDictionary<Type, Type> _modelBinderTypeMappings = new Dictionary<Type, Type>
        {
            {typeof(FilterOptionViewModel), typeof(FilterOptionViewModelBinder)}
        };

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (_modelBinderTypeMappings.ContainsKey(context.Metadata.ModelType))
            {
                return new BinderTypeModelBinder(_modelBinderTypeMappings[context.Metadata.ModelType]);
            }
            return null;
        }
    }
}
