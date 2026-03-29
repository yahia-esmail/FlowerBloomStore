
namespace FlowerBloomStore.Domain.Interfaces.ShoppingRepo
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart?> GetByUserIdAsync(string userId);
        Task<Cart?> GetWithItemsAsync(string userId);
        Task ClearCartAsync(string userId);
        Task<CartItem> GetCartItemByIdAsync(int cartItemId);
        Task UpdateAsync(CartItem item);
        Task DeleteAsync(CartItem item);
    }
}
