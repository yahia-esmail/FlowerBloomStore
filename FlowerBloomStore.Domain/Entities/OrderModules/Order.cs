

namespace FlowerBloomStore.Domain.Entities.OrderModules
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeliveryDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;


        // FKs:
        public string UserId { get; set; } = string.Empty;
        public int? AddressId { get; set; }

        // Navigation Properties:
        public virtual ApplicationUser? User { get; set; }
        public virtual Address? Address { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual Delivery? Delivery { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
