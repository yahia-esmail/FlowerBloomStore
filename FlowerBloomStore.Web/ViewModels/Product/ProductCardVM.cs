using System.ComponentModel.DataAnnotations;

namespace FlowerBloomStore.Web.ViewModels.Product
{
    public class ProductCardVM
    {
        public int Id { get; set; }
        [MaxLength(150)] public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }           // main image
        public decimal AverageRating { get; set; } = 0;
    }
}
