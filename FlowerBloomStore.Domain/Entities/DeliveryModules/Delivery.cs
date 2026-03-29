using FlowerBloomStore.Domain.Entities.OrderModules;
using FlowerBloomStore.Domain.Entities.UsersModule;

namespace FlowerBloomStore.Domain.Entities.DeliveryModules
{
    public class Delivery
    {
        public int Id { get; set; }
        public DeliveryStatus Status { get; set; } = DeliveryStatus.Preparing;
        public DateTime? DeliveredAt { get; set; }
        public string? TrackingCode { get; set; }

        // FKs:
        public int OrderId { get; set; }
        public int? DriverId { get; set; }

        // Navigation Properties:
        public virtual Order? Order { get; set; }
        public virtual Driver? Driver { get; set; }
    }
}
