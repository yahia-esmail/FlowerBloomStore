



namespace FlowerBloomStore.Application.Services
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;



        private readonly IProductImageRepository _productImageRepository;
        private readonly IWishlistRepository _wishlistRepository;



        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IProductImageRepository productImageRepository, IWishlistRepository wishlistRepository)

            : base((IRepository<Product>)productRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;

            _productImageRepository = productImageRepository;


            _wishlistRepository = wishlistRepository;
        }
        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        {
            return await _productRepository.GetByCategoryAsync(categoryId);
        }

        public async Task<Product?> GetProductDetailsAsync(int id)
        {
            return await _productRepository.GetDetailsAsync(id);
        }

        public async Task<ProductDetailsVM> GetByProductIdWithRelatedProductsAsync(int ProductId, string userId = null)
        {
            var product = await GetProductDetailsAsync(ProductId);
            if(product != null)
            {

                IEnumerable<Product> relatedProducts = await GetByCategoryAsync(product.CategoryId);

                IEnumerable<ProductImage> productImages = await _productImageRepository.getImagesByProductIdAsync(ProductId);
                relatedProducts = relatedProducts.Where(p => p.StockQuantity > 0 && p.Id != ProductId).Take(4);
                ProductDetailsVM vm = new ProductDetailsVM()
                {
                    Name = product.Name,
                    Price = product.Price,
                    Id = product.Id,
                    AverageRating = product.AverageRating,
                    Description = product.Description ?? "not found",
                    ImageUrl = product.ImageUrl ?? "not found",
                    ProductImages = productImages.ToList()

                };

                if(userId != null)
                {
                    vm.InWishlist = await _wishlistRepository.IsProductInWishlistAsync(userId, vm.Id);
                }

                foreach (var p in relatedProducts)
                {
                    ProductCardVM ProductCard = new ProductCardVM()
                    {
                        Name = p.Name,
                        Id = p.Id,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl
                    };
                    vm.RelatedProducts.Add(ProductCard);
                }
                return vm;
            }
            return null;
        }

        public async Task<ProductsGalleryVM> GenerateProductsGallery(List<int> _categoryIds = null, PriceOrder priceOrder = PriceOrder.None, int currentPage = 1)
        {
            ProductsGalleryVM productsGalleryVM = new ProductsGalleryVM();
           
            //Pagination 
            PaginationInfo PInfo = new PaginationInfo();

            PInfo.pageSize = 12;

            int totalProducts = await _productRepository.GetByCategoriesIDsCount(_categoryIds);
            PInfo.PagesCount = (int)Math.Ceiling((double)totalProducts / PInfo.pageSize); PInfo.currentPage = currentPage;
            PInfo.hasPrevious = currentPage > 1 ? true : false;
            PInfo.hasNext = currentPage == PInfo.PagesCount ? false : true;
            productsGalleryVM.PagesInfo = PInfo;


            //getting products
            IEnumerable<Product> products;
            if (priceOrder == PriceOrder.None)
            {
                products = await _productRepository.GetByCategoriesIDs(
                                categoryIds: _categoryIds,
                                pageSize: productsGalleryVM.PagesInfo.pageSize,
                                pageNumber: productsGalleryVM.PagesInfo.currentPage
                                );
            }
            else if(priceOrder == PriceOrder.HighToLow)
            {
                products = await _productRepository.GetByCategoriesIDsDesc(
                                categoryIds: _categoryIds,
                                pageSize: productsGalleryVM.PagesInfo.pageSize,
                                pageNumber: productsGalleryVM.PagesInfo.currentPage
                                );
            }
            else
            {
                products = await _productRepository.GetByCategoriesIDsAsc(
                                categoryIds: _categoryIds,
                                pageSize: productsGalleryVM.PagesInfo.pageSize,
                                pageNumber: productsGalleryVM.PagesInfo.currentPage
                                );

            }
            


            foreach (Product p in products)
            {
                ProductCardVM productCard = new ProductCardVM()
                {
                    Name = p.Name,
                    Id = p.Id,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl ?? "Url not found",
                };
                productsGalleryVM.Products.Add(productCard);
            }

            //ordering products
            //if(priceOrder != PriceOrder.None)
            //{
            //    if(priceOrder == PriceOrder.LowToHigh)
            //    {
            //        productsGalleryVM.Products = productsGalleryVM.Products.OrderBy(p => p.Price).ToList();
            //    }
            //    else
            //    {
            //       productsGalleryVM.Products = productsGalleryVM.Products.OrderByDescending(p => p.Price).ToList();
            //    }
            //}

            //getting all categories names
            var categories = await _categoryRepository.GetAllAsync();
            foreach(Category g in categories)
            {
                productsGalleryVM.CategoryNames.Add(new CategoryNameIdVM() { Id = g.Id, Name = g.Name });
            }
            return productsGalleryVM;
        }

        // Changed return type to match IProductService: Task<string?>
        public async Task<string?> GetFilteredProductsAsync(
            int? categoryId = null,
            string? searchTerm = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = ((IQueryable<Product>)_productRepository.GetQueryable());

            query = query.Include(p => p.Category);

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.Trim().ToLower();
                query = query.Where(p =>
                    p.Name.ToLower().Contains(searchTerm) ||
                    (p.Description != null && p.Description.ToLower().Contains(searchTerm)));
            }

            // Pagination
            var products = await query
                .OrderBy(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Serialize to JSON string (or any other string representation as needed)
            return System.Text.Json.JsonSerializer.Serialize(products);
        }
        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllWithCategoryAsync();
        }

    }
}