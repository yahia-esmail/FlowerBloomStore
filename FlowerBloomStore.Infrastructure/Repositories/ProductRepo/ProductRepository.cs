

namespace FlowerBloomStore.Infrastructure.Repositories.ProductRepo
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext _context)
            : base(_context) { }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        {
            return await _dbset
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product?> GetDetailsAsync(int id)
        {
            return await _dbset
                .Include(p => p.Category)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetByCategoriesIDs(List<int>? categoryIds, int pageNumber, int pageSize)
        {
            if(categoryIds != null && categoryIds.Any())
            {
               return await _dbset.Where(p => categoryIds.Contains(p.CategoryId)).Skip((pageNumber-1) * pageSize).Take(pageSize).ToListAsync();
            }
            return await _dbset.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }


        public async Task<IEnumerable<Product>> GetByCategoriesIDsAsc(List<int>? categoryIds, int pageNumber, int pageSize)
        {
            if (categoryIds != null && categoryIds.Any())
            {
                return await _dbset.Where(p => categoryIds.Contains(p.CategoryId)).OrderBy(p=>p.Price).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            return await _dbset.OrderBy(p => p.Price).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }


        public async Task<IEnumerable<Product>> GetByCategoriesIDsDesc(List<int>? categoryIds, int pageNumber, int pageSize)
        {
            if (categoryIds != null && categoryIds.Any())
            {
                return await _dbset.Where(p => categoryIds.Contains(p.CategoryId)).OrderByDescending(p => p.Price).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            return await _dbset.OrderByDescending(p => p.Price).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetByCategoriesIDsCount(List<int>? categoryIds)
        {

            if (categoryIds != null && categoryIds.Any())
            {
                return _dbset.Where(p => categoryIds.Contains(p.CategoryId)).Count();
            }
            return  _dbset.Count();
        }


        public object GetQueryable()
        {
            return _dbset.AsQueryable();
        }
        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

    }
}
