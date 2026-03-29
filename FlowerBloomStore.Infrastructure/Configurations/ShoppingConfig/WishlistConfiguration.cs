using FlowerBloomStore.Domain.Entities.ShoppingModules;

namespace FlowerBloomStore.Infrastructure.Configurations.ShoppingConfig
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.ToTable("Wishlists");

            builder.HasKey(w => w.Id);

            builder.Property(w => w.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(w => w.UserId)
                   .IsRequired();

            builder.HasOne(w => w.User)
                   .WithOne(u => u.Wishlist)
                   .HasForeignKey<Wishlist>(w => w.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(w => w.UserId)
                   .IsUnique();
        }
    }
}
