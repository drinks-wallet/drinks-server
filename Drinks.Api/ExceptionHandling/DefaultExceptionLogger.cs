using System.Web.Http.ExceptionHandling;

namespace Drinks.Api.ExceptionHandling
{
    public class DefaultExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            ApiLogger.Log(context.ExceptionContext.Exception.ToString());
        }
    }
}