using FlowerBloomStore.Application.Interfaces.Products;

namespace FlowerBloomStore.Application.Services.Products

{
   
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> repository)
            : base(repository)
        {
            _categoryRepository = repository;
        }
        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            var categories = await _categoryRepository.FindAsync(c => c.Name == name);
            return categories.FirstOrDefault();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesWithProductCountAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
                ImageUrl = c.ImageUrl,
                Products = c.Products ?? new List<FlowerBloomStore.Domain.Entities.ProductModules.Product>()
            }).ToList();
        }

        public Task<Category?> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllWithProductCountAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

  
            return categories.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
                ImageUrl = c.ImageUrl,
                Products = c.Products ?? new List<FlowerBloomStore.Domain.Entities.ProductModules.Product>()
            }).ToList();
        }

        
    }
}
