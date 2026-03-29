using FlowerBloomStore.Application.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace FlowerBloomStore.Web.ViewComponents
{
 
    public class FeaturedCategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

       
        public FeaturedCategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

         public async Task<IViewComponentResult> InvokeAsync(int count = 6)
        {
             var categories = await _categoryService.GetAllWithProductCountAsync();

            var featured = categories.Take(count).ToList();

            return View(featured);
        }
    }
}