using FlowerBloomStore.Application.Enums;
using FlowerBloomStore.Application.Interfaces;
using FlowerBloomStore.Application.Interfaces.Products;
using FlowerBloomStore.Application.ViewModels;
using FlowerBloomStore.Domain.Entities.ProductModules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowerBloomStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: /Product
        public async Task<IActionResult> Index(int? categoryId)
        {
            //if (categoryId.HasValue)
            //{
            //    var products = await _productService.GetByCategoryAsync(categoryId.Value);
            //    return View(products);
            //}

            //var allProducts = await _productService.GetAllAsync();
            if (categoryId.HasValue)
            {
                var productsGallery = await _productService.GenerateProductsGallery(new List<int>() { categoryId.Value });
                productsGallery.SelectedCatgorieIDs.Add(categoryId.Value);
                return View(productsGallery);
            }
            else
            {
                var productsGallery = await _productService.GenerateProductsGallery();
                return View(productsGallery);
            }
        }
   





        public async Task<IActionResult> FilteredProducts(ProductsGalleryVM productsGalleryVM, int currentPage = 1)
        {
            
            List<int> currentSelectedCategories = productsGalleryVM.SelectedCatgorieIDs;
            PriceOrder currentPriceOrder = productsGalleryVM.SelectedPriceOrder;
            if (productsGalleryVM.SelectedCatgorieIDs.Count() == 0)
            {
                productsGalleryVM = await _productService.GenerateProductsGallery(priceOrder : productsGalleryVM.SelectedPriceOrder, currentPage: currentPage);
                productsGalleryVM.SelectedCatgorieIDs = currentSelectedCategories;
                productsGalleryVM.SelectedPriceOrder = currentPriceOrder;
                return View("Index",productsGalleryVM);   
            }
            else
            {
                productsGalleryVM = await _productService.GenerateProductsGallery(productsGalleryVM.SelectedCatgorieIDs,  productsGalleryVM.SelectedPriceOrder, currentPage: currentPage);
                productsGalleryVM.SelectedCatgorieIDs = currentSelectedCategories;
                productsGalleryVM.SelectedPriceOrder = currentPriceOrder;
                return View("Index", productsGalleryVM);
            }
        }


        // GET: /Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            //var product = await _productService.GetProductDetailsAsync(id);
            //if (product == null) return NotFound();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var productWithRelated = await _productService.GetByProductIdWithRelatedProductsAsync(id, userId);
            if (productWithRelated == null) return NotFound();
            return View("Details",productWithRelated);
        }

        // GET: /Product/Create (Admin)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Product model)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: /Product/Edit/5 (Admin)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: /Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Product model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: /Product/Delete/5 (Admin)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}