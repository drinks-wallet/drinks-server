using System.Web.Http.ExceptionHandling;
using Drinks.DI;
using Drinks.Services;

namespace Drinks.Api.ExceptionHandling
{
    public class DefaultExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            MvcDependencyInjectorContainer.Resolve<ILogService>().Log(context.ExceptionContext.Exception.ToString());
        }
    }
}