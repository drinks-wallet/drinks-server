using Drinks.Repository;
using System.Collections.Generic;
using Drinks.Entities;
using System.Linq;

namespace Drinks.Services
{
    using JetBrains.Annotations;

    public interface IProductsService
    {
        [NotNull]
        IEnumerable<Product> GetAllProducts();
        [CanBeNull]
        Product GetProduct(byte id);
    }

    [UsedImplicitly]
    public class ProductsService : IProductsService
    {
        readonly IDrinksContext _drinksContext;

        public ProductsService(IUnitOfWork unitOfWork)
        {
            _drinksContext = unitOfWork.DrinksContext;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _drinksContext.Products;
        }

        public Product GetProduct(byte id)
        {
            return _drinksContext.Products.Find(id);
        }
    }
}