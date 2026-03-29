using FlowerBloomStore.Application.ViewModels;
using FlowerBloomStore.Domain.Entities.ProductModules;
namespace FlowerBloomStore.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> FeaturedProducts { get; set; }
        public IEnumerable<ProductsGalleryVM> ProductsGallery { get; set; }
    }
}