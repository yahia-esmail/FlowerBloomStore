using FlowerBloomStore.Domain.Entities.ProductModules;
using System.ComponentModel.DataAnnotations;

namespace FlowerBloomStore.Web.ViewModels.Product
{
    public class ProductDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }           // main image
        public decimal AverageRating { get; set; } = 0;
        public List<ProductCardVM> relatedProducts = new List<ProductCardVM>();
    }
}
