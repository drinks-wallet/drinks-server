using System;

namespace Drinks.Entities.Extensions
{
    public static class DateTimeExtensions
    {
        public static int ToUnixTimestamp(this DateTime time)
        {
            var span = (time - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            return (int)span.TotalSeconds;
        }
    }
}