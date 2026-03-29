using FlowerBloomStore.Domain.Interfaces.PaymentRepo;

namespace FlowerBloomStore.Infrastructure.Repositories.PaymentRepo
{
    public class PaymentRepository : Repository<Payment> , IPaymentRepository
    {
        public PaymentRepository(AppDbContext _context)
        :base(_context){}
    }
}
