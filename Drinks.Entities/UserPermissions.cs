namespace Drinks.Entities
{
    using System;

    [Flags]
    public enum UserPermissions : byte
    {
        None = 0x0,
        CanMonitorPurchases = 0x1,
        IsAdmin = 0x2
    }
}
