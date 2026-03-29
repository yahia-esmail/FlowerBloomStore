using FlowerBloomStore.Domain.Interfaces.UserRepo;
namespace FlowerBloomStore.Infrastructure.Repositories.UserRepo
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(AppDbContext context) : base(context)
        {
        }


    }
}
