using FlowerBloomStore.Application.Interfaces.Products;
using FlowerBloomStore.Domain.Interfaces;
using FlowerBloomStore.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowerBloomStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;

        private readonly IProductService _productService;
        private readonly IWishlistRepository wishlist;
        public HomeController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesWithProductCountAsync();

            var allProducts = await _productService.GetAllAsync();
            var featuredProducts = allProducts.Take(8);

            var model = new HomeViewModel
            {
                Categories = categories,
                FeaturedProducts = featuredProducts
            };

            return View(model);
        }
    }
}