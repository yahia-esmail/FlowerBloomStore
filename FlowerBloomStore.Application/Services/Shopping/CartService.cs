


namespace FlowerBloomStore.Application.Services
{
    public class CartService : GenericService<Cart>, ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository; 

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
            : base(cartRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<Cart?> GetUserCartAsync(string userId)
        {
            return await _cartRepository.GetByUserIdAsync(userId);
        }

        public async Task<Cart?> GetUserCartWithItemsAsync(string userId)
        {
            return await _cartRepository.GetWithItemsAsync(userId);
        }

        public async Task ClearUserCartAsync(string userId)
        {
            await _cartRepository.ClearCartAsync(userId);
        }

        public async Task AddItemToCartAsync(string userId, int productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null || product.StockQuantity < quantity) return;

            var cart = await _cartRepository.GetByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await _cartRepository.AddAsync(cart);
                await _cartRepository.SaveChangesAsync();
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = product.Price,
                    CartId = cart.Id
                });
            }

            _cartRepository.Update(cart);
            await _cartRepository.SaveChangesAsync();
        }

        public async Task RemoveItemFromCartAsync(string userId, int cartItemId)
        {
            var cart = await _cartRepository.GetByUserIdAsync(userId);
            if (cart == null) return;

            var item = cart.Items.FirstOrDefault(i => i.Id == cartItemId);
            if (item != null)
            {
                cart.Items.Remove(item);
                _cartRepository.Update(cart);
                await _cartRepository.SaveChangesAsync();
            }
        }
        public async Task IncreaseQuantityAsync(int cartItemId)
        {
            var item = await _cartRepository.GetCartItemByIdAsync(cartItemId);

            if (item == null)
                throw new Exception("Cart item not found");

            item.Quantity++;

            await _cartRepository.UpdateAsync(item);
        }

        public async Task DecreaseQuantityAsync(int cartItemId)
        {
            var item = await _cartRepository.GetCartItemByIdAsync(cartItemId);

            if (item == null)
                throw new Exception("Cart item not found");

            if (item.Quantity > 1)
            {
                item.Quantity--;
                await _cartRepository.UpdateAsync(item);
            }
            else
            {
                await _cartRepository.DeleteAsync(item);
            }
        }
    }
}