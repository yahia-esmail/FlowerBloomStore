

namespace FlowerBloomStore.Domain.Interfaces.CategoryRepo
{
    public interface ICategoryRepository : IRepository<Category> {
        Task<Category?> GetByNameAsync(string name);
        Task<IEnumerable<Category>> GetAllWithProductCountAsync();
    }
   
}
