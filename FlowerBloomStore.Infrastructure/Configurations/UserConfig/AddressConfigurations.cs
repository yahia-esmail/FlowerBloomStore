using FlowerBloomStore.Domain.Entities.UsersModule;

namespace FlowerBloomStore.Infrastructure.Configurations.UserConfig
{
    internal class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.City)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(a => a.Street)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(a => a.Building)
                   .HasMaxLength(50);

            builder.Property(a => a.IsDefault)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(a => a.UserId)
                   .IsRequired();

            builder.HasOne(a => a.User)
                   .WithMany(u => u.Addresses)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
