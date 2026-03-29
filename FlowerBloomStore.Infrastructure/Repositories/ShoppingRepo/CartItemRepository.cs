

namespace FlowerBloomStore.Infrastructure.Repositories.ShoppingRepo
{
    internal class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(AppDbContext context) : base(context) { }
    }
}
