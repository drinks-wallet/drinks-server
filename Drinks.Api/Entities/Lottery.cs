using System;

namespace Drinks.Api.Entities
{
    public static class Lottery
    {
        static readonly Random Random = new Random();

        public static bool IsFree()
        {
            return Random.Next(50) == 0;
        }
    }
}