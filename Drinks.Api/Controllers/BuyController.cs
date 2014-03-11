using System;
using System.Globalization;
using System.Web.Http;
using Drinks.Api.Entities;
using Drinks.Entities;
using Drinks.Entities.Exceptions;
using Drinks.Services;

namespace Drinks.Api.Controllers
{

    public class BuyController : ApiController
    {
        readonly ITransactionService _transactionService;
        readonly IUserService _userService;
        readonly IProductsService _productsService;
        readonly ILogService _logService;

        public BuyController(ITransactionService transactionService, IUserService userService, IProductsService productsService, ILogService logService)
        {
            _transactionService = transactionService;
            _userService = userService;
            _productsService = productsService;
            _logService = logService;
        }

        public BuyResponse Post(BuyRequest request)
        {
            if (request == null)
                return new BuyResponse(BuyResponseStatus.DeserializationException);

            User user;
            decimal balance;
            var isFree = Lottery.IsFree();
            try
            {
                user = _userService.GetUserByBadge(request.Badge);
                request.Validate(ConfigurationFacade.RemoteHashKey);
                balance = _transactionService.Buy(request);
            }
            catch (InvalidBadgeException)
            {
                return new BuyResponse(request.Badge);
            }
            catch (InvalidHashException)
            {
                return new BuyResponse(BuyResponseStatus.InvalidHash);
            }
            catch (InvalidProductException)
            {
                return new BuyResponse(BuyResponseStatus.InvalidProduct);
            }
            catch (InvalidTimestampException)
            {
                return new BuyResponse(BuyResponseStatus.InvalidTimestamp);
            }
            catch (InsufficientFundsException)
            {
                return new BuyResponse(BuyResponseStatus.InsufficientFunds);
            }

            var balanceString = balance.ToString("N", CultureInfo.InvariantCulture.NumberFormat);
            var validResponse = new BuyResponse(user.Name, balanceString, request.Product);
            if (!isFree)
                return validResponse;

            try
            {
                Reimburse(request, user.Id);
            }
            catch (Exception e)
            {
                _logService.Log(e.ToString());
                return validResponse;
            }

            return new BuyResponse(BuyResponseStatus.Free);
        }

        void Reimburse(BuyRequest request, int userId)
        {
            var price = _productsService.GetProduct(request.Product).Price;
            var reimbursementRequest = new ReloadRequest(price, userId, -1);
            _transactionService.Reload(reimbursementRequest);
        }
    }
}