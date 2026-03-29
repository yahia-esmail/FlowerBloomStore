namespace FlowerBloomStore.Domain.Interfaces.OrderRepo
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetUserOrdersAsync(string userId);
        Task<IEnumerable<Order>> GetByUserIdAsync(string userId);
        Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status);
        Task<Order?> GetOrderWithItemsAsync(int orderId);
        Task<decimal> GetTotalSalesAsync(DateTime? from = null, DateTime? to = null);
        IQueryable<Order> GetQueryable();
        Task<Order?> GetOrderDetailsAsync(int orderId);
        //Task<Order?> GetOrderWithItemsAsync(string orderId);

        //Task<Order?> GetActiveOrderAsync(string userId);
    }
}
