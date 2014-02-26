using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Drinks.Web.Filters
{
    public class AdminOnlyAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (!UserContext.User.IsAdmin)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext) { }
    }
}