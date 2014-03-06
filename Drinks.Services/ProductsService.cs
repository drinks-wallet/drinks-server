using System.Collections.Generic;
using Drinks.Entities;
using Drinks.Entities.Exceptions;
using Drinks.Repository;
using JetBrains.Annotations;

namespace Drinks.Services
{
    public interface IProductsService
    {
        [NotNull]
        IEnumerable<Product> GetAllProducts();
        [NotNull]
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
            var product = _drinksContext.Products.Find(id);
            if (product == null)
                throw new InvalidProductException();

            return product;
        }
    }
}