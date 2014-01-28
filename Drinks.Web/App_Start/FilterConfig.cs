using System.Web.Mvc;

namespace Drinks.Web.App_Start
{
    using Drinks.Web.Filters;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AuthorizeAttribute());
            filters.Add(new UserLoadedAttribute());
        }
    }
}