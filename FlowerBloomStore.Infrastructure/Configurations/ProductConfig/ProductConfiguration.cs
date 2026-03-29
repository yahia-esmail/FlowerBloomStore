using FlowerBloomStore.Domain.Entities.ProductModules;

namespace FlowerBloomStore.Infrastructure.Configurations.ProductConfig
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(p => p.Description)
                   .HasMaxLength(2000);

            builder.Property(p => p.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.StockQuantity)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(p => p.ImageUrl)
                   .HasMaxLength(500);

            builder.Property(p => p.AverageRating)
                   .HasColumnType("decimal(3,2)")
                   .HasDefaultValue(0);

            builder.Property(p => p.CategoryId)
                   .IsRequired();

            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(t =>
                t.HasCheckConstraint("CK_Product_Price_NonNegative", "[Price] >= 0")
            );

            builder.ToTable(t =>
                t.HasCheckConstraint("CK_Product_Stock_NonNegative", "[StockQuantity] >= 0")
            );
        }
    }
}
