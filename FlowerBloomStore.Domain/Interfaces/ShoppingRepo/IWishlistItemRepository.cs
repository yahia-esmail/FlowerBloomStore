using FlowerBloomStore.Domain.Entities;

namespace FlowerBloomStore.Domain.Interfaces
{
    public interface IWishlistItemRepository : IRepository<WishlistItem>
    {
        Task<IEnumerable<WishlistItem>> GetByWishlistIdAsync(int wishlistId);
        Task<WishlistItem?> GetByProductAndWishlistAsync(int wishlistId, int productId);
    }
}