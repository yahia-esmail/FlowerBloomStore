
namespace FlowerBloomStore.Application.Interfaces.Products
{
    public interface ICategoryService:IService<Category>
    {
        Task<IEnumerable<Category>> GetAllCategoriesWithProductCountAsync();
        Task<IEnumerable<Category>> GetAllWithProductCountAsync();
        Task<Category?> GetByNameAsync(string name);
    }
}
