

namespace FlowerBloomStore.Application.Services.Orders
{
    public class OrderService : GenericService<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
            : base(orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(string userId)
        {
            return await _orderRepository.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            return await _orderRepository.GetByStatusAsync(status);
        }

        public async Task<Order?> GetOrderDetailsAsync(int orderId)
        {
            return await _orderRepository.GetOrderWithItemsAsync(orderId);
        }

        public async Task<decimal> GetTotalSalesAsync(DateTime? from = null, DateTime? to = null)
        {
            return await _orderRepository.GetTotalSalesAsync(from, to);
        }
        public async Task<IEnumerable<Order>> GetFilteredOrdersAsync(
            OrderStatus? status = null,
            string? userId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
           
            var query = _orderRepository.GetQueryable()
                .Include(o => o.User)          
                .Include(o => o.Items)         
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.Payment)     
                .Include(o => o.Address)       
                .AsQueryable();

            if (status.HasValue)
                query = query.Where(o => o.Status == status.Value);

            if (!string.IsNullOrWhiteSpace(userId))
                query = query.Where(o => o.UserId == userId);

            if (fromDate.HasValue)
                query = query.Where(o => o.OrderDate >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(o => o.OrderDate <= toDate.Value);

            query = query.OrderByDescending(o => o.OrderDate);

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetFilteredOrdersCountAsync(
            OrderStatus? status = null,
            string? userId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            var query = _orderRepository.GetQueryable();

            if (status.HasValue)
                query = query.Where(o => o.Status == status.Value);

            if (!string.IsNullOrWhiteSpace(userId))
                query = query.Where(o => o.UserId == userId);

            if (fromDate.HasValue)
                query = query.Where(o => o.OrderDate >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(o => o.OrderDate <= toDate.Value);

            return await query.CountAsync();
        }
        public async Task<double> GetOrdersCountByStatusAsync(OrderStatus status)
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Count(o => o.Status == status);
        }
        
    }
}