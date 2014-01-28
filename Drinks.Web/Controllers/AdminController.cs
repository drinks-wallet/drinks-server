using System.Web.Mvc;

namespace Drinks.Web.Controllers
{
    using System;
    using System.Runtime.Remoting.Messaging;
    using Drinks.Entities;
    using Drinks.Entities.Exceptions;
    using Drinks.Services;
    using Drinks.Web.Filters;
    using Drinks.Web.Helpers;
    using Drinks.Web.Models.Admin;

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
        [RequiredPrivileges(UserPermissions.CanCreateAccounts)]
        public ActionResult CreateAccount()
        {
            var tempData = new TempDataHelper(TempData);
            return string.IsNullOrWhiteSpace(tempData.SuccessMessage) ? View() : View(new CreateAccountModel(tempData.SuccessMessage));
        }

        [HttpPost]
        [RequiredPrivileges(UserPermissions.CanCreateAccounts)]
        public ActionResult CreateAccount(CreateAccountModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var user = new User(model.Name, model.Username, model.BadgeId, model.UserPrivileges.ToPriviliges());
            try
            {
                _userService.CreateUser(user, model.Username);
            }
            catch (UserExistsException)
            {
                ModelState.AddModelError("", "The user already exists.");
                throw;
            }

            var tempData = new TempDataHelper(TempData);
            tempData.SuccessMessage = "The account was created successfully!";
            return RedirectToAction("CreateAccount");
        }

        [HttpGet]
        [RequiredPrivileges(UserPermissions.CanAddMoney)]
        public ActionResult AddMoney()
        {
            var users = _userService.GetAllUsers();
            var tempData = new TempDataHelper(TempData);
            return string.IsNullOrWhiteSpace(tempData.SuccessMessage) ?
                View(new AddMoneyModel(users)) :
                View(new AddMoneyModel(tempData.SuccessMessage, users));
        }

        [HttpPost]
        [RequiredPrivileges(UserPermissions.CanAddMoney)]
        public ActionResult AddMoney(AddMoneyModel model)
        {
            if (!ModelState.IsValid)
                return View(new AddMoneyModel(_userService.GetAllUsers()));
            
            // ReSharper disable once PossibleInvalidOperationException
            var transaction = new ReloadRequest(model.Amount.Value, model.UserId, UserContext.User.Id);

            _transactionService.Reload(transaction);
            var tempData = new TempDataHelper(TempData);
            tempData.SuccessMessage = _userService.GetUser(model.UserId).Name + "'s account has been credited. The current balance is " +
                                      _userService.GetBalance(model.UserId).ToString("C");
            return RedirectToAction("AddMoney");
        }

        [HttpGet]
        [RequiredPrivileges(UserPermissions.CanCreateAccounts)]
        public ActionResult EditAccount()
        {
            return View(new EditAccountModel(_userService.GetAllUsers()));
        }

        [HttpPost]
        [RequiredPrivileges(UserPermissions.CanCreateAccounts)]
        public ActionResult EditAccount(EditAccountModel model)
        {
            if (!ModelState.IsValid)
                return View();

            return RedirectToAction("EditAccount", true);
        }
	}
}