

namespace FlowerBloomStore.Domain.Entities.ProductModules
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(150)] public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }           // main image
        public decimal AverageRating { get; set; } = 0;

        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public virtual ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
        // public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        // public virtual ICollection<StockHistory> StockHistory { get; set; } = new List<StockHistory>();
    }
}
