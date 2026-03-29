namespace FlowerBloomStore.Domain.Entities.ProductModules
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public bool IsMain { get; set; }

        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}
