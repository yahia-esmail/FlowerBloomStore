using FlowerBloomStore.Domain.Entities.OrderModules;

namespace FlowerBloomStore.Infrastructure.Configurations.OrderConfig
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.OrderDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(o => o.TotalAmount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(o => o.DiscountAmount)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(o => o.Status)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(o => o.UserId)
                   .IsRequired();

            builder.Property(o => o.AddressId);

            builder.HasOne(o => o.User)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Address)
                   .WithMany()
                   .HasForeignKey(o => o.AddressId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.ToTable(t =>
                t.HasCheckConstraint("CK_Order_TotalAmount_NonNegative", "[TotalAmount] >= 0")
            );


        }
    }
}
