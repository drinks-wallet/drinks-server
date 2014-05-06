using System;
using Drinks.Entities;
using Drinks.Entities.Exceptions;
using Drinks.Repository;

namespace Drinks.Services
{
    public interface ITransactionService
    {
        decimal Reload(ReloadRequest reloadRequest);
        decimal Buy(BuyRequest request);
    }

    public class TransactionService : ITransactionService
    {
        readonly IUserService _userService;
        readonly IProductsService _productsService;
        readonly IDrinksContext _drinksContext;

        readonly object _transactionLock = new object();

        public TransactionService(IUnitOfWork unitOfWork, IUserService userService, IProductsService productsService)
        {
            _userService = userService;
            _productsService = productsService;
            _drinksContext = unitOfWork.DrinksContext;
        }

        public decimal Reload(ReloadRequest request)
        {
            // TODO: Robustify this (maybe in the DB with a stored procedure).
            lock (_transactionLock)
            {
                _drinksContext.Transactions.Add(GenerateTransaction(request));
                _drinksContext.SaveChanges();
            }

            return _userService.GetBalance(request.UserId);
        }

        public decimal Buy(BuyRequest request)
        {
            int userId;
            lock (_transactionLock)
            {
                var transaction = GenerateTransaction(request);
                userId = transaction.UserId;
                _drinksContext.Transactions.Add(transaction);
                _drinksContext.SaveChanges();
            }

            return _userService.GetBalance(userId);
        }

        Transaction GenerateTransaction(BuyRequest request)
        {
            var user = _userService.GetUserByBadge(request.Badge);
            if (user == null)
                throw new InvalidBadgeException();
            var balance = _userService.GetBalance(user.Id);
            var product = _productsService.GetProduct(request.Product);
            if (product == null)
                throw new InvalidProductException();
            if (product.Price > balance)
                throw new InsufficientFundsException();
            var price = ApplyDiscount(product.Price, user.DiscountPercentage);
            return new Transaction(-price, user.Id, user.Id);
        }

        Transaction GenerateTransaction(ReloadRequest request)
        {
            var executingUser = _userService.GetUser(request.ExecutorUserId);
            if (executingUser == null)
                throw new InvalidOperationException("Invalid user ID.");
            if (!executingUser.IsAdmin)
                throw new InsufficientPermissionsException();
            return new Transaction(request.Amount, request.UserId, request.ExecutorUserId);
        }

        static decimal ApplyDiscount(decimal price, decimal discountPercentage)
        {
            return discountPercentage == 0 ? price : price - price * discountPercentage / 100;
        }
    }
}