using FlowerBloomStore.Domain.Entities.UsersModule;

namespace FlowerBloomStore.Infrastructure.Configurations.UserConfig
{
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FullName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(u => u.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(u => u.Gender)
                   .IsRequired(false)
                   .HasConversion<string>();

            // for Optimize search operation
            builder.HasIndex(u => u.FullName);

            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Users_Gender", "[Gender] in ('male', 'female')");
            });
        }
    }
}
