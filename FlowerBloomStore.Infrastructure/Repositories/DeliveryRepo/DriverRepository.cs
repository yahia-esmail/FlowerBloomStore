namespace FlowerBloomStore.Infrastructure.Repositories.DeliveryRepo
{
    public class DriverRepository : Repository<Driver>, IDriverRepository
    {
        public DriverRepository(AppDbContext _context)
            : base(_context) { }



    }
}
