using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerBloomStore.Application.Services.Shopping
{
    public class WishListService : IWishListService
    {
        private readonly IWishlistRepository wishRepo;

        public WishListService(IWishlistRepository _wishRepo )
        {
            wishRepo = _wishRepo;
        }
        public Task AddAsync(Domain.Entities.ShoppingModules.Wishlist entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<Domain.Entities.ShoppingModules.Wishlist, bool>>? predicate = null)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Expression<Func<Domain.Entities.ShoppingModules.Wishlist, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Entities.ShoppingModules.Wishlist>> FindAsync(Expression<Func<Domain.Entities.ShoppingModules.Wishlist, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Entities.ShoppingModules.Wishlist>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.ShoppingModules.Wishlist?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Domain.Entities.ShoppingModules.Wishlist entity)
        {
            throw new NotImplementedException();
        }
    }
}
