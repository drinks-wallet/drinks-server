namespace Drinks.Web.Models.Shared
{
    using Drinks.Entities;

    public class UserPrivilegesModel
    {
        public bool CanMonitorPurchases { get; set; }
        public bool CanCreateAccounts { get; set; }
        public bool CanAssignBadges { get; set; }
        public bool CanAddMoney { get; set; }

        public UserPermissions ToPriviliges()
        {
            var permissions = UserPermissions.None;
            if (CanMonitorPurchases)
                permissions = permissions | UserPermissions.CanMonitorPurchases;
            if (CanCreateAccounts)
                permissions = permissions | UserPermissions.CanCreateAccounts;
            if (CanAssignBadges)
                permissions = permissions | UserPermissions.CanAssignBadges;
            if (CanAddMoney)
                permissions = permissions | UserPermissions.CanAddMoney;

            return permissions;
        }
    }
}