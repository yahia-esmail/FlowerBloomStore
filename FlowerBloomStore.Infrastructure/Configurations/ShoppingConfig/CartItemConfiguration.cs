using FlowerBloomStore.Domain.Entities.ShoppingModules;

namespace FlowerBloomStore.Infrastructure.Configurations.ShoppingConfig
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Quantity)
                   .IsRequired()
                   .HasDefaultValue(1);

            builder.Property(ci => ci.UnitPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(ci => ci.Cart)
                   .WithMany(c => c.Items)
                   .HasForeignKey(ci => ci.CartId)
                   .OnDelete(DeleteBehavior.Cascade);

            
            builder.HasOne(ci => ci.Product)  
                   .WithMany(p => p.CartItems)
                   .HasForeignKey(ci => ci.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
