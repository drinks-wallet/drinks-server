using System.Web.Mvc;
using Drinks.Services;
using Drinks.Entities.Exceptions;
using Drinks.Web.Helpers;
using Drinks.Web.Models.Account;

namespace Drinks.Web.Controllers
{
    public class AccountController : Controller
    {
        readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var balance = _userService.GetBalance(UserContext.User.Id);
            return View(balance);
        }
        
        [HttpGet]
        public ActionResult MyProfile()
        {
            var tempData = new TempDataFacade(TempData);
            return string.IsNullOrWhiteSpace(tempData.SuccessMessage) ? View() : View(new MyProfileModel(tempData.SuccessMessage));
        }

        [HttpPost]
        public ActionResult MyProfile(MyProfileModel model)
        {
            if (!ModelState.IsValid)
                return View();
            
            try
            {
                _userService.ChangePassword(UserContext.User.Id, model.OldPassword, model.NewPassword);
            }
            catch (InvalidUserCredentialsException)
            {
                ModelState.AddModelError("", "The old password is invalid.");
                return View();
            }
            
            var tempData = new TempDataFacade(TempData);
            tempData.SuccessMessage = "Your password has been changed.";
            return RedirectToAction("MyProfile");
        }

        [HttpGet]
        public JsonResult ValidatePassword(string oldPassword)
        {
            if (UserContext.User == null)
                return null;

            try
            {
                _userService.ValidateUser(UserContext.User.Username, oldPassword);
            }
            catch (InvalidUserCredentialsException)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
	}
}