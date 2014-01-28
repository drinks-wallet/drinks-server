using System;

namespace Drinks.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IDrinksContext DrinksContext { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        readonly IDrinksContext _drinksContext;

        public UnitOfWork(IDrinksContext drinksContext)
        {
            _drinksContext = drinksContext;
        }

        public IDrinksContext DrinksContext { get { return _drinksContext; } }

        public void Dispose()
        {
            _drinksContext.SaveChanges();
            _drinksContext.Dispose();
        }
    }
}
