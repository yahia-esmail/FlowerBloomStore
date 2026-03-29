using System.ComponentModel.DataAnnotations;

namespace FlowerBloomStore.Domain.Entities.UsersModule
{
    public class Address
    {
        public int Id { get; set; }

        [MaxLength(50)] public string City { get; set; } = string.Empty;
        [MaxLength(100)] public string Street { get; set; } = string.Empty;
        [MaxLength(50)] public string Building { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public bool IsDefault { get; set; }

        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }
    }
}
