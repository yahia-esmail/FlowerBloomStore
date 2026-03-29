


namespace FlowerBloomStore.Infrastructure.Repositories.OrderRepo;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext _context) 
        : base(_context) { }

    //public async Task<Order?> GetOrderWithItemsAsync(string orderId)
    //    => await _dbset.FindAsync(orderId);

    public async Task<IEnumerable<Order>> GetUserOrdersAsync(string userId)
        => await _dbset.Where(o => o.UserId == userId).ToListAsync();
    public async Task<IEnumerable<Order>> GetByUserIdAsync(string userId)
    {
        return await _dbset
            .Where(o => o.UserId == userId)
            .Include(o => o.Items)
            .Include(o => o.Payment)
            .Include(o => o.Delivery)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status)
    {
        return await _dbset
            .Where(o => o.Status == status)
            .Include(o => o.User)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderWithItemsAsync(int orderId)
    {
        return await _dbset
            .Include(o => o.Items).ThenInclude(oi => oi.Product)
            .Include(o => o.Payment)
            .Include(o => o.Delivery)
            .Include(o => o.Address)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<decimal> GetTotalSalesAsync(DateTime? from = null, DateTime? to = null)
    {
        var query = _dbset.AsQueryable();

        if (from.HasValue) query = query.Where(o => o.OrderDate >= from.Value);
        if (to.HasValue) query = query.Where(o => o.OrderDate <= to.Value);

        return await query.SumAsync(o => o.TotalAmount - o.DiscountAmount);
    }
    public IQueryable<Order> GetQueryable()
    {
        return _dbset.AsQueryable(); 
    }
    public async Task<Order?> GetOrderDetailsAsync(int orderId)
    {
        return await _dbset
            .Include(o => o.User)                        
            .Include(o => o.Payment)                      
            .Include(o => o.Address)                     
            .Include(o => o.Delivery)                      
            .Include(o => o.Items)                       
                .ThenInclude(oi => oi.Product)            
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }
}
