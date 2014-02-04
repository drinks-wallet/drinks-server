namespace Drinks.Web.Models.Shared
{
    using Drinks.Entities;

    public class UserPrivilegesModel
    {
        public bool CanMonitorPurchases { get; set; }
        public bool IsAdmin { get; set; }

        public UserPermissions ToPriviliges()
        {
            var permissions = UserPermissions.None;
            if (CanMonitorPurchases)
                permissions = permissions | UserPermissions.CanMonitorPurchases;
            if (IsAdmin)
                permissions = permissions | UserPermissions.IsAdmin;

            return permissions;
        }
    }
}