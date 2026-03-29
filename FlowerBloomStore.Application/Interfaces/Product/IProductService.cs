

namespace FlowerBloomStore.Application.Interfaces.Products
{
    public interface IProductService:IService<Product>
    {
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
        Task<Product?> GetProductDetailsAsync(int id);
        //Task<productdetailsvm?> GetProductDetailsAsync(int id);
        Task<ProductDetailsVM> GetByProductIdWithRelatedProductsAsync(int ProductId, string userId = null);
        //Task<ProductsGalleryVM> GenerateProductsGallery(List<int> categoryIds = null, PriceOrder priceOrder = PriceOrder.None);
        Task<ProductsGalleryVM> GenerateProductsGallery(List<int> _categoryIds = null, PriceOrder priceOrder = PriceOrder.None, int currentPage = 1);

        Task<string?> GetFilteredProductsAsync(int? categoryId, string? searchTerm, int pageNumber, int pageSize);

    }
}
