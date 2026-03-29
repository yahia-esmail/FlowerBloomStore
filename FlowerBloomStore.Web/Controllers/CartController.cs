
using Microsoft.EntityFrameworkCore;

namespace FlowerBloomStore.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        // GET: /Cart
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var userId = "1";
            var cart = await _cartService.GetUserCartWithItemsAsync(userId);
            return View(cart);
        }

        // POST: /Cart/Add/5?quantity=2
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var userId = "1";
            await _cartService.AddItemToCartAsync(userId, productId, quantity);
            //return RedirectToAction(nameof(Index));
            TempData["Success"] = "Added successfully to cart!";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        // POST: /Cart/Remove/5
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            //var userId = "1";
            await _cartService.RemoveItemFromCartAsync(userId, cartItemId);
            return RedirectToAction(nameof(Index));
        }

        // POST: /Cart/Clear
        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var userId = "1";

            await _cartService.ClearUserCartAsync(userId);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> IncreaseQuantity(int cartItemId)
        {
            await _cartService.IncreaseQuantityAsync(cartItemId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseQuantity(int cartItemId)
        {
            await _cartService.DecreaseQuantityAsync(cartItemId);
            return RedirectToAction("Index");
        }

    }
}