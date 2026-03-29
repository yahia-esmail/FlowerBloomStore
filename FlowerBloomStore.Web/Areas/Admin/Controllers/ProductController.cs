
using FlowerBloomStore.Domain.Entities.ProductModules;


namespace FlowerBloomStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int? categoryId, int page = 1)
        {
            const int pageSize = 5;
            IEnumerable<Product> productsList;

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                productsList = await _productService.GetByCategoryAsync(categoryId.Value);
            }
            else
            {
                productsList = await _productService.GetAllAsync();
            }

            var total = productsList.Count();
            var products = productsList
                .OrderBy(p => p.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(total / (double)pageSize);
            ViewBag.TotalItems = total;
            ViewBag.PageSize = pageSize;
            ViewBag.CategoryId = categoryId;

            return View(products);
        }
        // GET: /Admin/Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetByProductIdWithRelatedProductsAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // GET: /Admin/Product/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View();
        }

        // POST: /Admin/Product/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product model, IFormFile? imageFile)
        {
            if (model.CategoryId == 0 && Request.Form.ContainsKey("CategoryId"))
            {
                if (int.TryParse(Request.Form["CategoryId"], out int catId))
                {
                    model.CategoryId = catId;
                }
            }
            Console.WriteLine($"CategoryId من الـ form = {model.CategoryId}");
            TempData["DebugCategoryId"] = $"CategoryId = {model.CategoryId}";
            ModelState.Remove("Category");
            if (model.CategoryId <= 0)
            {
                ModelState.AddModelError("CategoryId", "يرجى اختيار فئة صالحة");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(model);
            }
            
                if (imageFile != null && imageFile.Length > 0)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");
                    if (!Directory.Exists(directoryPath))
                        Directory.CreateDirectory(directoryPath);

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var path = Path.Combine(directoryPath, fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    model.ImageUrl = "/images/products/" + fileName;
                }
                await _productService.AddAsync(model);
                TempData["Success"] = "تمت إضافة المنتج بنجاح";
                return RedirectToAction(nameof(Index));
          
        }

        // GET: /Admin/Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(product);
        }

        // POST: /Admin/Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product model, IFormFile? imageFile)
        {
            if (id != model.Id) return NotFound();
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            if (ModelState.IsValid)
            {
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.StockQuantity = model.StockQuantity;
                product.CategoryId = model.CategoryId;
               
                if (imageFile != null && imageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    product.ImageUrl = "/images/products/" + fileName;
                }
                await _productService.UpdateAsync(product); 
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(model);
        }
        // GET: /Admin/Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: /Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}