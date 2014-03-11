using Drinks.DI;
using Drinks.Services;
using JetBrains.Annotations;

namespace Drinks.Api
{
    [UsedImplicitly]
    public static class ApiLogger
    {
        [UsedImplicitly]
        public static void Log(string message)
        {
            DependencyInjectorContainerBase.Container.GetInstance<ILogService>().Log(message);
        }
    }
}