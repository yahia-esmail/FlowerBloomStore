

namespace FlowerBloomStore.Domain.Entities.ProductModules
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(100)] 
        public string Name { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
