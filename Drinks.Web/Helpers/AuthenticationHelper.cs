using System;

namespace Drinks.Web.Helpers
{
    using System.Web.Security;
    using Drinks.Entities;
    using Drinks.Services;

    public class AuthenticationHelper
    {
        readonly IUserService _userService;

        public AuthenticationHelper(IUserService userService)
        {
            _userService = userService;
        }

        public static void DeauthenticateUser()
        {
            FormsAuthentication.SignOut();
            UserContext.User = null;
        }

        public void AuthenticateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("username");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("password");

            AuthenticateUser(_userService.ValidateUser(username, password));
        }

        static void AuthenticateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            UserContext.User = user;
            FormsAuthentication.SetAuthCookie(UserContext.User.Id.ToString(), true);
        }
    }
}