using FlowerBloomStore.Domain.Entities.OrderModules;

namespace FlowerBloomStore.Infrastructure.Configurations.OrderConfig
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.Quantity)
                   .IsRequired()
                   .HasDefaultValue(1);

            builder.Property(oi => oi.UnitPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.Items)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.Product)
                   .WithMany(p => p.OrderItems)
                   .HasForeignKey(oi => oi.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(t =>
                t.HasCheckConstraint("CK_OrderItem_Quantity_Positive", "[Quantity] > 0")
            );

            builder.ToTable(t =>
                t.HasCheckConstraint("CK_OrderItem_UnitPrice_NonNegative", "[UnitPrice] >= 0")
            );


        }
    }
}
