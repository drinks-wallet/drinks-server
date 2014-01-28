namespace Drinks.Web.App_Start
{
    using System.Web.Mvc;
    using Drinks.Web.CustomModelBinders;

    public class ModelBinderConfig
    {
        internal static void RegisterModelBinders()
        {
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
        }
    }
}