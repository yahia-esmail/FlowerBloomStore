using System.ComponentModel.DataAnnotations;

namespace FlowerBloomStore.Domain.Entities.DeliveryModules
{
    public class Driver
    {
        public int Id { get; set; }
        [MaxLength(100)] public string Name { get; set; } = string.Empty;
        [MaxLength(20)] public string Phone { get; set; } = string.Empty;
        public VehicleType VehicleType { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
    }
}
