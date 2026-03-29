namespace FlowerBloomStore.Domain.Interfaces.DeliveryRepo
{
    public interface IDeliveryRepository 
    {
        Task<List<Delivery>> GetPendingDeliveries();
    }
}
