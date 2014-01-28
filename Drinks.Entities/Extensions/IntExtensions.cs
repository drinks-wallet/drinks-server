using System;

namespace Drinks.Entities.Extensions
{
    public static class IntExtensions
    {
        public static DateTime FromUnixTimestamp(this int unixTimestamp)
        {
            return new DateTime(1970, 1, 1).ToLocalTime().AddSeconds(unixTimestamp);
        }
    }
}