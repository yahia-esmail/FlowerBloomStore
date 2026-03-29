using FlowerBloomStore.Domain.Entities.ProductModules;

namespace FlowerBloomStore.Infrastructure.Configurations.ProductConfig
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");

            builder.HasKey(pi => pi.Id);

            builder.Property(pi => pi.Url)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(pi => pi.IsMain)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.HasOne(pi => pi.Product)
                   .WithMany(p => p.Images)
                   .HasForeignKey(pi => pi.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
