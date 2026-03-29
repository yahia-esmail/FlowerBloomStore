namespace FlowerBloomStore.Domain.Entities.ShoppingModules
{
    public class Wishlist
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // FKs:
        public string UserId { get; set; } = string.Empty;

        // Navigation Properties:
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<WishlistItem> Items { get; set; } = new List<WishlistItem>();
    }
}
