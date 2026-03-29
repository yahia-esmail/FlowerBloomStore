using FlowerBloomStore.Domain.Interfaces.DeliveryRepo;

namespace FlowerBloomStore.Application.Services.Delivery
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository deliveryRepository;
        public DeliveryService()
        {
            
        }
    }
}
