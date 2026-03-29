
using FlowerBloomStore.Application.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace FlowerBloomStore.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: /Category
        public async Task<IActionResult> Index()
        {

            var categories = await _categoryService.GetAllWithProductCountAsync();
            return View(categories);
        }

        // GET: /Category/Details/5
        //public async Task<IActionResult> Details(int id)
        //{
        //    var category = await _categoryService.GetByIdAsync(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(category);
        //}


    }
}