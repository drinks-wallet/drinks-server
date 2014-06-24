namespace Drinks.Entities
{
    public class BuyReceipt
    {
        public readonly decimal NewBalance;
        public readonly decimal AmountDebited;

        public BuyReceipt(decimal newBalance, decimal amountDebited)
        {
            NewBalance = newBalance;
            AmountDebited = amountDebited;
        }
    }
}