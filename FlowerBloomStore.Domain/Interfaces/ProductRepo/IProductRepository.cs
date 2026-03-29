
namespace FlowerBloomStore.Domain.Interfaces.ProductRepo
{

    public interface IProductRepository:IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
        Task<Product?> GetDetailsAsync(int id);
        //Task<IEnumerable<Product>> GetByCategoriesIDs(List<int>? categoryIds = null);
        Task<IEnumerable<Product>> GetByCategoriesIDs(List<int>? categoryIds, int pageNumber, int pageSize);
        Task<int> GetByCategoriesIDsCount(List<int>? categoryIds);
        Task<IEnumerable<Product>> GetByCategoriesIDsDesc(List<int>? categoryIds, int pageNumber, int pageSize);
        Task<IEnumerable<Product>> GetByCategoriesIDsAsc(List<int>? categoryIds, int pageNumber, int pageSize);

        object GetQueryable();
        Task<IEnumerable<Product>> GetAllWithCategoryAsync();

    }

}
