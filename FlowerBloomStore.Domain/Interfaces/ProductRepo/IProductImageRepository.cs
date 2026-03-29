

namespace FlowerBloomStore.Domain.Interfaces.ProductRepo
{
    public interface IProductImageRepository 
    {
        Task<IEnumerable<ProductImage>> getImagesByProductIdAsync(int Id);

    }
}
