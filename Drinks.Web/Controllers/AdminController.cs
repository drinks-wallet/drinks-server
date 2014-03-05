using System.Web.Mvc;
using Drinks.Entities;
using Drinks.Entities.Exceptions;
using Drinks.Services;
using Drinks.Web.Filters;
using Drinks.Web.Helpers;
using Drinks.Web.Models.Admin;

namespace Drinks.Web.Controllers
{
    public class AdminController : Controller
    {
        readonly IUserService _userService;
        readonly ITransactionService _transactionService;

        public AdminController(IUserService userService, ITransactionService transactionService)
        {
            _userService = userService;
            _transactionService = transactionService;
        }

        [HttpGet]
        [AdminOnly]
        public ActionResult CreateAccount()
        {
            var tempData = new TempDataFacade(TempData);
            return string.IsNullOrWhiteSpace(tempData.SuccessMessage) ? View() : View(new CreateAccountModel(tempData.SuccessMessage));
        }

        [HttpPost]
        [AdminOnly]
        public ActionResult CreateAccount(CreateAccountModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var user = new User(model.Name, model.Username, model.BadgeId, model.IsAdmin);
            try
            {
                _userService.CreateUser(user, model.Username);
            }
            catch (UserExistsException)
            {
                ModelState.AddModelError("", "The user already exists.");
                throw;
            }

            var tempData = new TempDataFacade(TempData);
            tempData.SuccessMessage = "The account was created successfully!";
            return RedirectToAction("CreateAccount");
        }

        [HttpGet]
        [AdminOnly]
        public ActionResult AddMoney()
        {
            var users = _userService.GetAllUsers();
            var tempData = new TempDataFacade(TempData);
            return string.IsNullOrWhiteSpace(tempData.SuccessMessage) ?
                View(new AddMoneyModel(users)) :
                View(new AddMoneyModel(tempData.SuccessMessage, users));
        }

        [HttpPost]
        [AdminOnly]
        public ActionResult AddMoney(AddMoneyModel model)
        {
            if (!ModelState.IsValid)
                return View(new AddMoneyModel(_userService.GetAllUsers()));

            // ReSharper disable once PossibleInvalidOperationException
            var transaction = new ReloadRequest(model.Amount.Value, model.UserId, UserContext.User.Id);

            _transactionService.Reload(transaction);
            var tempData = new TempDataFacade(TempData);
            tempData.SuccessMessage = _userService.GetUser(model.UserId).Name + "'s account has been credited. The current balance is " +
                                      _userService.GetBalance(model.UserId).ToString("C");
            return RedirectToAction("AddMoney");
        }

        [HttpGet]
        [AdminOnly]
        public ActionResult EditAccount(int? selectedUserId)
        {
            var tempData = new TempDataFacade(TempData);
            return View(new EditAccountModel(_userService.GetAllUsers(), selectedUserId) { SuccessMessage = tempData.SuccessMessage });
        }

        [HttpPost]
        [AdminOnly]
        public ActionResult EditAccount(EditAccountModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var user = _userService.GetUser(model.Id);
            user.IsAdmin = model.IsAdmin;
            if (!string.IsNullOrWhiteSpace(model.BadgeId))
                user.BadgeId = model.BadgeId;
            if (!string.IsNullOrWhiteSpace(model.Name))
                user.Name = model.Name;
            if (!string.IsNullOrWhiteSpace(model.Username))
                user.Username = model.Username;
            if (!string.IsNullOrWhiteSpace(model.Password))
                _userService.ResetPassword(user, model.Password);
            
            var tempData = new TempDataFacade(TempData);
            tempData.SuccessMessage = model.Name + "'s account has been updated.";
            return RedirectToAction("EditAccount", new { selectedUserId = model.Id });
        }

        [HttpGet]
        [AdminOnly]
        public JsonResult GetUserData(int userId)
        {
            var user = _userService.GetUser(userId);
            return Json(new
                        {
                            userId = user.Id,
                            name = user.Name,
                            username = user.Username,
                            badgeId = user.BadgeId,
                            isAdmin = user.IsAdmin
                        },
                        JsonRequestBehavior.AllowGet);
        }
    }
}