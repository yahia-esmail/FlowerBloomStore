
namespace FlowerBloomStore.Infrastructure.Repositories.UserRepo
{
    public class AddressRepository:Repository<Address>,IAddressRepository
    {
        public AddressRepository(AppDbContext _context)
       : base(_context) { }
    }
}
