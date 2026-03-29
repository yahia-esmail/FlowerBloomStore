using FlowerBloomStore.Domain.Entities.ProductModules;

namespace FlowerBloomStore.Domain.Entities.ShoppingModules
{
    public class WishlistItem
    {
        public int Id { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        // FKs:
        public int WishlistId { get; set; }
        public int ProductId { get; set; }

        // Navigation Properties:
        public virtual Wishlist? Wishlist { get; set; }
        public virtual Product? Product { get; set; }
    }
}
