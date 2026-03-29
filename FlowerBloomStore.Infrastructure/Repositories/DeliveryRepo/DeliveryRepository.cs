namespace FlowerBloomStore.Infrastructure.Repositories.DeliveryRepo
{
    public class DeliveryRepository : Repository<Delivery>, IDeliveryRepository
    {
        public DeliveryRepository(AppDbContext _context)
            : base(_context) { }


        public Task<List<Delivery>> GetPendingDeliveries()
        {
            throw new NotImplementedException();
        }

    }
}