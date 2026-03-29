

namespace FlowerBloomStore.Infrastructure.Repositories.ProductRepository.ProductRepo
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext _context)
        : base(_context) { }
        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _dbset
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<Category>> GetAllWithProductCountAsync()
        {
            return await _context.Categories
         .Include(c => c.Products)
         .ToListAsync();
        }


        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}