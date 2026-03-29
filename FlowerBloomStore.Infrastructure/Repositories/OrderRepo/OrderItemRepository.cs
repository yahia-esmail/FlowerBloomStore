namespace FlowerBloomStore.Infrastructure.Repositories.OrderRepo
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext _context) 
            : base(_context) { }


    }
}
