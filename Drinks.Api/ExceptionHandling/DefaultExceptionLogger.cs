using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drinks.Api.ExceptionHandling
{
    using System.Web.Http.ExceptionHandling;

    public class DefaultExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            System.Diagnostics.EventLog.WriteEntry("Drinks.Api", context.ExceptionContext.Exception.ToString());
        }
    }
}