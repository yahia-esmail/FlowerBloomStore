using FlowerBloomStore.Domain.Interfaces.ProductRepo;

namespace FlowerBloomStore.Infrastructure.Repositories.ProductRepo
{
    public class ProductImageRepository: Repository<ProductImage>,IProductImageRepository
    {
        public ProductImageRepository(AppDbContext _context)
        :base(_context){}

        public async Task<IEnumerable<ProductImage>> getImagesByProductIdAsync(int Id)
        {
            return await _dbset.Where(i => i.ProductId == Id).ToListAsync();
        }
    }
}
