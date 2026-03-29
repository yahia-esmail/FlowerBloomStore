

namespace FlowerBloomStore.Domain.Entities.ShoppingModules
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }

        public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
