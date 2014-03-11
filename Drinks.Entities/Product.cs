using JetBrains.Annotations;

namespace Drinks.Entities
{
    public class Product
    {
        [UsedImplicitly]
        public byte Id { get; set; }
        [UsedImplicitly]
        public string Name { get; set; }
        [UsedImplicitly]
        public decimal Price { get; set; }

        public override string ToString()
        {
            return Name + " " + Price.ToString("#0.00");
        }
    }
}