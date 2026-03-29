using FlowerBloomStore.Domain.Entities.DeliveryModules;

namespace FlowerBloomStore.Infrastructure.Configurations.DeliveryConfig
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("Drivers");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(d => d.Phone)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(d => d.VehicleType)
                   .HasConversion<string>();

        }
    }
}
