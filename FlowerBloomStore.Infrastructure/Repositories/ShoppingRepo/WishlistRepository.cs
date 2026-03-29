using Microsoft.EntityFrameworkCore;
using FlowerBloomStore.Domain.Entities;
using FlowerBloomStore.Domain.Interfaces;
using FlowerBloomStore.Infrastructure.Data;

namespace FlowerBloomStore.Infrastructure.Repositories
{
    public class WishlistRepository : Repository<Wishlist>, IWishlistRepository
    {
        public WishlistRepository(AppDbContext context) : base(context) { }

        public async Task<Wishlist?> GetByUserIdAsync(string userId)
        {
            return await _dbset
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task<Wishlist?> GetWithItemsAsync(string userId)
        {
            return await _dbset
                .Include(w => w.Items)
                    .ThenInclude(wi => wi.Product)
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task<bool> IsProductInWishlistAsync(string userId, int productId)
        {
            return await _dbset
                .AnyAsync(w => w.UserId == userId &&
                               w.Items.Any(wi => wi.ProductId == productId));
        }

        public async Task AddProductToWishlistAsync(string userId, int productId)
        {
            var wishlist = await GetByUserIdAsync(userId);
            if (wishlist == null)
            {
                wishlist = new Wishlist { UserId = userId };
                await AddAsync(wishlist);
                await SaveChangesAsync();
            }

            if (!wishlist.Items.Any(i => i.ProductId == productId))
            {
                wishlist.Items.Add(new WishlistItem
                {
                    ProductId = productId,
                    WishlistId = wishlist.Id
                });
                Update(wishlist);
                await SaveChangesAsync();
            }
        }

        public async Task RemoveProductFromWishlistAsync(string userId, int productId)
        {
            var wishlist = await GetByUserIdAsync(userId);
            if (wishlist == null) return;

            var item = wishlist.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                wishlist.Items.Remove(item);
                Update(wishlist);
                await SaveChangesAsync();
            }
        }
    }
}