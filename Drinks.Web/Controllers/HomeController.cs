using System.Web.Mvc;
using Drinks.Entities.Exceptions;
using Drinks.Services;
using Drinks.Web.Helpers;
using Drinks.Web.Models.Home;

namespace Drinks.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(IndexModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var authenticationHelper = new AuthenticationHelper(_userService);
            try
            {
                authenticationHelper.AuthenticateUser(model.Username, model.Password);
            }
            catch (InvalidUserCredentialsException)
            {
                ModelState.AddModelError("", "Please enter a valid username and password.");
                return View();
            }
            
            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public RedirectToRouteResult LogOut()
        {
            AuthenticationHelper.DeauthenticateUser();
            return RedirectToAction("Index", "Home");
        }
	}
}