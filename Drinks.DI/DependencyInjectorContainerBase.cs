using Drinks.Repository;
using Drinks.Services;
using SimpleInjector;

namespace Drinks.DI
{
    public abstract class DependencyInjectorContainerBase
    {
        public static T Resolve<T>()
            where T : class
        {
            return Container.GetInstance<T>();
        }
        
        public static Container Container { get; protected set; }

        protected static void RegisterServices()
        {
            Container.RegisterPerWebRequest<IProductsService, ProductsService>();
            Container.RegisterPerWebRequest<ITransactionService, TransactionService>();
            Container.RegisterPerWebRequest<IUserService, UserService>();
            Container.RegisterPerWebRequest<IDrinksContext, DrinksContext>();
            Container.RegisterPerWebRequest<IPasswordHelper, PasswordHelper>();
            Container.RegisterPerWebRequest<IUnitOfWork, UnitOfWork>();
            Container.RegisterPerWebRequest<ILogService, LogService>();
        }
    }
}
