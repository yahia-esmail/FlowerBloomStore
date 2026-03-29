namespace FlowerBloomStore.Domain.Interfaces
{
    public interface IWishlistRepository : IRepository<Wishlist>
    {
        Task<Wishlist?> GetByUserIdAsync(string userId);
        Task<Wishlist?> GetWithItemsAsync(string userId);
        Task<bool> IsProductInWishlistAsync(string userId, int productId);
        Task AddProductToWishlistAsync(string userId, int productId);
        Task RemoveProductFromWishlistAsync(string userId, int productId);
    }
}