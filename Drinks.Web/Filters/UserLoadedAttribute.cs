using System.Linq;

namespace Drinks.Web.Filters
{
    using System.Web.Mvc;
    using System.Web.Mvc.Filters;
    using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

    public class UserLoadedAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (!filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any() &&
                !filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any() &&
                UserContext.User == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext) { }
    }
}