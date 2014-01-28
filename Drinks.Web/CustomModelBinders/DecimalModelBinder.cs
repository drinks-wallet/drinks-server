using System;
using System.Globalization;
using System.Web.Mvc;

namespace Drinks.Web.CustomModelBinders
{
    public class DecimalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var modelState = new ModelState { Value = valueResult };
            object actualValue = null;
            try
            {
                var correctedValue = valueResult.AttemptedValue.Replace(',', '.');
                actualValue = decimal.Parse(correctedValue, CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }
}