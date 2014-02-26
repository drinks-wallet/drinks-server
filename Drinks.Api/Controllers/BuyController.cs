using System.Web.Http;
using Drinks.Api.Entities;
using Drinks.Entities;
using Drinks.Entities.Exceptions;
using Drinks.Services;

namespace Drinks.Api.Controllers
{
    using System.Globalization;

    public class BuyController : ApiController
    {
        readonly ITransactionService _transactionService;
        readonly IUserService _userService;

        public BuyController(ITransactionService transactionService, IUserService userService)
        {
            _transactionService = transactionService;
            _userService = userService;
        }

        public BuyResponse Post(BuyRequest request)
        {
            if (request == null)
                return new BuyResponse(BuyResponseStatus.DeserializationException);

            decimal balance;
            try
            {
                request.Validate(ConfigurationFacade.RemoteHashKey);
                balance = _transactionService.Buy(request);
            }
            catch (InvalidBadgeException)
            {
                return new BuyResponse(BuyResponseStatus.InvalidBadge, badgeId: request.Badge);
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

            var user = _userService.GetUserByBadge(request.Badge);
            // TODO: Get rid of the thousands separator.
            return user != null ?
                new BuyResponse(BuyResponseStatus.Valid, user.Name, balance.ToString("N", CultureInfo.InvariantCulture.NumberFormat)) :
                new BuyResponse(BuyResponseStatus.Valid);
        }
    }
}
