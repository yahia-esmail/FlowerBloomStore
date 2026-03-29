

namespace FlowerBloomStore.Infrastructure.Repositories.ShoppingRepo
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context){}
        public async Task<Cart?> GetByUserIdAsync(string userId)
        {
            return await _dbset
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Cart?> GetWithItemsAsync(string userId)
        {
            return await _dbset
                .Include(c => c.Items).ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task ClearCartAsync(string userId)
        {
            var cart = await GetByUserIdAsync(userId);
            if (cart == null) return;

            _context.CartItems.RemoveRange(cart.Items);
            await SaveChangesAsync();
        }
        public async Task<CartItem> GetCartItemByIdAsync(int id)
        {
            return await _context.CartItems.FindAsync(id);
        }

        public async Task UpdateAsync(CartItem item)
        {
            _context.CartItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CartItem item)
        {
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }

    }
}
