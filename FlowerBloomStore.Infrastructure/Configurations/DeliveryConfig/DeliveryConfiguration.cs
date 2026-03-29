using FlowerBloomStore.Domain.Entities.DeliveryModules;

namespace FlowerBloomStore.Infrastructure.Configurations.DeliveryConfig
{
    public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.ToTable("Deliveries");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Status)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(d => d.DeliveredAt);

            builder.Property(d => d.TrackingCode)
                   .HasMaxLength(100);

            builder.HasOne(d => d.Order)
                   .WithOne(o => o.Delivery)
                   .HasForeignKey<Delivery>(d => d.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Driver)
                   .WithMany(dr => dr.Deliveries)
                   .HasForeignKey(d => d.DriverId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
