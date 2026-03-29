using FlowerBloomStore.Domain.Entities.ShoppingModules;

namespace FlowerBloomStore.Infrastructure.Configurations.ShoppingConfig
{
    public class WishlistItemConfiguration : IEntityTypeConfiguration<WishlistItem>
    {
        public void Configure(EntityTypeBuilder<WishlistItem> builder)
        {
            builder.ToTable("WishlistItems");

            builder.HasKey(wi => wi.Id);

            builder.Property(wi => wi.AddedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(wi => wi.Wishlist)
                   .WithMany(w => w.Items)
                   .HasForeignKey(wi => wi.WishlistId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(wi => wi.Product)
                   .WithMany(p => p.WishlistItems)
                   .HasForeignKey(wi => wi.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
