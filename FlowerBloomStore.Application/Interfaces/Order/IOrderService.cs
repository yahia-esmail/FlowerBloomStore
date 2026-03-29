
namespace FlowerBloomStore.Application.Interfaces.Orders
{
    public interface IOrderService : IService<Order>
    {
        //Task<int> PlaceOrder(CreateOrderDto dto);
        //Task CancelOrder(int orderId);
        //Task<List<OrderDto>> GetUserOrders(string userId);
        Task<IEnumerable<Order>> GetUserOrdersAsync(string userId);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
        Task<Order?> GetOrderDetailsAsync(int orderId);
        Task<decimal> GetTotalSalesAsync(DateTime? from = null, DateTime? to = null);
        Task<double> GetOrdersCountByStatusAsync(OrderStatus pending);
        Task<IEnumerable<Order>> GetFilteredOrdersAsync(
            OrderStatus? status = null,
            string? userId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int pageNumber = 1,
            int pageSize = 10);
        Task<int> GetFilteredOrdersCountAsync(
            OrderStatus? status = null,
            string? userId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null);
    }
}
