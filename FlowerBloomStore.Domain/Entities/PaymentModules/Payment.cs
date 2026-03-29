using FlowerBloomStore.Domain.Entities.OrderModules;

namespace FlowerBloomStore.Domain.Entities.PaymentModules
{
    public class Payment
    {
        public int Id { get; set; }
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.None;
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public DateTime? PaidAt { get; set; }
        public string? TransactionId { get; set; }

        // FKs:
        public int OrderId { get; set; }

        // Navigation Properties:
        public virtual Order? Order { get; set; }

    }
}
