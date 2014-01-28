namespace Drinks.Entities
{
    public class Product
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return Name + " - " + Price.ToString("#0.00");
        }
    }
}