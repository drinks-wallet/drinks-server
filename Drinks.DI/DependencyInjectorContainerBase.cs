using Drinks.Repository;
using Drinks.Services;
using SimpleInjector;

namespace Drinks.DI
{
    public abstract class DependencyInjectorContainerBase
    {
        protected static readonly Container Container = new Container();

        static DependencyInjectorContainerBase()
        {
            RegisterServices();
        }

        public static T Resolve<T>()
            where T : class
        {
            return Container.GetInstance<T>();
        }
        
        static void RegisterServices()
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
