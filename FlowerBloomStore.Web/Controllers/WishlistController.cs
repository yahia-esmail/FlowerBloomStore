using System.Security.Claims;
using FlowerBloomStore.Application.Interfaces;
using FlowerBloomStore.Domain.Entities.ShoppingModules;
using FlowerBloomStore.Domain.Interfaces;
using FlowerBloomStore.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerBloomStore.Web.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly IWishlistRepository _wishlistService;

        public WishlistController(IWishlistRepository wishlistService)
        {
            _wishlistService = wishlistService;
        }

        // GET: /Wishlist
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wishlist = await _wishlistService.GetByUserIdAsync(userId);
            return View(wishlist);
        }

        //POST: /Wishlist/Add/5
        //[HttpPost]
        //public async Task<IActionResult> AddToWishlist(int productId)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    await _wishlistService.AddProductToWishlistAsync(userId, productId);
        //    return RedirectToAction(nameof(Index));
        //}
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToWishlist(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool alreadyInWishlist = await _wishlistService.IsProductInWishlistAsync(userId, productId);

            string message;
            bool isAdded;

            if (alreadyInWishlist)
            {
                await _wishlistService.RemoveProductFromWishlistAsync(userId, productId);
                message = "تمت الإزالة من المفضلة";
                isAdded = false;
            }
            else
            {
                await _wishlistService.AddProductToWishlistAsync(userId, productId);
                message = "تمت الإضافة إلى المفضلة";
                isAdded = true;
            }

            // نفترض إنها نجحت (لو فيه exception هيتعامل معاه try-catch لو حطيته)
            return Json(new
            {
                success = true,
                isAdded,
                message
            });
        }
        //}        // POST: /Wishlist/Remove/5
        [HttpPost]
        public async Task<IActionResult> RemoveFromWishlist(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _wishlistService.RemoveProductFromWishlistAsync(userId, productId);
            return RedirectToAction(nameof(Index));
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToWishlistOk(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool alreadyInWishlist = await _wishlistService.IsProductInWishlistAsync(userId, productId);

            string message;
            bool isAdded;

            if (alreadyInWishlist)
            {
                await _wishlistService.RemoveProductFromWishlistAsync(userId, productId);
                message = "تمت الإزالة من المفضلة";
                isAdded = false;
            }
            else
            {
                await _wishlistService.AddProductToWishlistAsync(userId, productId);
                message = "تمت الإضافة إلى المفضلة";
                isAdded = true;
            }

            // نفترض إنها نجحت (لو فيه exception هيتعامل معاه try-catch لو حطيته)
            return Json(new
            {
                success = true,
                isAdded,
                message
            });
        }
    }
}

