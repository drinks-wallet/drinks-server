using System.Web;

namespace Drinks.Web
{
    using System.Globalization;
    using Drinks.Entities;

    public static class UserContext
    {
        const string UserSessionKey = "User";
        const string CultureHttpItemsKey = "Culture";

        public static User User
        {
            
            get
            {
                if (HttpContext.Current.Session != null)
                    return (User)HttpContext.Current.Session[UserSessionKey];
                return (User)HttpContext.Current.Items[UserSessionKey];
            }
            set
            {
                if (HttpContext.Current.Session != null)
                    HttpContext.Current.Session.Add(UserSessionKey, value);
                else
                    HttpContext.Current.Items.Add(UserSessionKey, value);
            }
        }

        public static CultureInfo Culture
        {
            get { return (CultureInfo)HttpContext.Current.Items[CultureHttpItemsKey]; }
            set { HttpContext.Current.Items[CultureHttpItemsKey] = value; }
        }
    }
}