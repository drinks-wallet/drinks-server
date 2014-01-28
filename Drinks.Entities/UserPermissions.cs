namespace Drinks.Entities
{
    using System;

    [Flags]
    public enum UserPermissions : byte
    {
        None = 0x0,
        CanMonitorPurchases = 0x1,
        CanCreateAccounts = 0x2,
        CanAssignBadges = 0x4,
        CanAddMoney = 0x8
    }
}
