using FlowerBloomStore.Domain.Entities.ShoppingModules;

namespace FlowerBloomStore.Infrastructure.Configurations.ShoppingConfig
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(c => c.UserId)
                   .IsRequired();

            builder.HasOne(c => c.User)
                   .WithOne(u => u.Cart)
                   .HasForeignKey<Cart>(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(c => c.UserId)
                   .IsUnique();
        }
    }
}
