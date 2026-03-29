

namespace FlowerBloomStore.Domain.Entities.OrderModules
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // FKs:
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        // Navigation Properties:
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
