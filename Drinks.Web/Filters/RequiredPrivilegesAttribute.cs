using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Drinks.Entities;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace Drinks.Web.Filters
{
    public class RequiredPrivilegesAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        readonly UserPermissions _requiredPermissions;

        public RequiredPrivilegesAttribute(UserPermissions requiredPermissions)
        {
            _requiredPermissions = requiredPermissions;
        }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (Enum.GetValues(typeof(UserPermissions)).Cast<UserPermissions>()
                .Where(privilege => _requiredPermissions.HasFlag(privilege))
                .Any(privilege => !UserContext.User.Permissions.HasFlag(privilege)))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext) { }
    }
}