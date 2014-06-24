namespace Drinks.Entities
{
    public class BuyReceipt
    {
        public readonly decimal NewBalance;
        public readonly decimal TransactionAmount;

        public BuyReceipt(decimal newBalance, decimal transactionAmount)
        {
            NewBalance = newBalance;
            TransactionAmount = transactionAmount;
        }
    }
}