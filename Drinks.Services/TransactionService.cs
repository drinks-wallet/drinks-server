using System;
using Drinks.Entities;
using Drinks.Entities.Exceptions;
using Drinks.Repository;

namespace Drinks.Services
{
    public interface ITransactionService
    {
        decimal Reload(ReloadRequest reloadRequest);
        BuyReceipt Buy(BuyRequest request);
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
            lock (_transactionLock)
            {
                _drinksContext.Transactions.Add(GenerateTransaction(request));
                _drinksContext.SaveChanges();
            }

            return _userService.GetBalance(request.UserId);
        }

        public BuyReceipt Buy(BuyRequest request)
        {
            Transaction transaction;
            lock (_transactionLock)
            {
                transaction = GenerateTransaction(request);
                _drinksContext.Transactions.Add(transaction);
                _drinksContext.SaveChanges();
            }
            
            var newBalance = _userService.GetBalance(transaction.UserId);
            return new BuyReceipt(newBalance, -transaction.Amount);
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
            var price = ApplyDiscount(product.Price, user.DiscountPercentage);
            if (price > balance)
                throw new InsufficientFundsException();
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