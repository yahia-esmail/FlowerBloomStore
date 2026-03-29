using Microsoft.EntityFrameworkCore;
using FlowerBloomStore.Domain.Entities;
using FlowerBloomStore.Domain.Interfaces;
using FlowerBloomStore.Infrastructure.Data;

namespace FlowerBloomStore.Infrastructure.Repositories
{
    public class WishlistItemRepository : Repository<WishlistItem>, IWishlistItemRepository
    {
        public WishlistItemRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<WishlistItem>> GetByWishlistIdAsync(int wishlistId)
        {
            return await _dbset
                .Where(wi => wi.WishlistId == wishlistId)
                .Include(wi => wi.Product)
                .ToListAsync();
        }

        public async Task<WishlistItem?> GetByProductAndWishlistAsync(int wishlistId, int productId)
        {
            return await _dbset
                .FirstOrDefaultAsync(wi => wi.WishlistId == wishlistId && wi.ProductId == productId);
        }
    }
}