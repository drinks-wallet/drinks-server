using Drinks.Entities.Logging;
using Drinks.Repository;
using JetBrains.Annotations;

namespace Drinks.Services
{
    public interface ILogService
    {
        [UsedImplicitly]
        void Log([NotNull] string message);
    }

    [UsedImplicitly]
    public class LogService : ILogService
    {
        readonly IDrinksContext _drinksContext;

        public LogService(IUnitOfWork unitOfWork)
        {
            _drinksContext = unitOfWork.DrinksContext;
        }

        public void Log(string message)
        {
            _drinksContext.Log.Add(new LogItem(message));
        }
    }
}