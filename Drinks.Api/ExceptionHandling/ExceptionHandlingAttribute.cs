using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drinks.Api.ExceptionHandling
{
    using System.Web.Http.Filters;
    using Drinks.DI;
    using Drinks.Services;

    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            WebApiDependencyInjectorContainer.Resolve<ILogService>().Log(actionExecutedContext.Exception.ToString());
        }
    }
}