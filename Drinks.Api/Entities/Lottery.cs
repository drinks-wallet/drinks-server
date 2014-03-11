using System;

namespace Drinks.Api.Entities
{
    public static class Lottery
    {
        const int Odds = 1;

        static readonly Random Random = new Random();

        public static bool IsFree()
        {
            return Random.Next(Odds) == 0;
        }
    }
}