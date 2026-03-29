

namespace FlowerBloomStore.Application.Interfaces.Shopping
{
    public interface ICartService:IService<Cart>
    {
        Task<Cart?> GetUserCartAsync(string userId);
        Task<Cart?> GetUserCartWithItemsAsync(string userId);
        Task ClearUserCartAsync(string userId);
        Task AddItemToCartAsync(string userId, int productId, int quantity);
        Task RemoveItemFromCartAsync(string userId, int cartItemId);

        Task IncreaseQuantityAsync(int cartItemId);
        Task DecreaseQuantityAsync(int cartItemId);

    }
}
