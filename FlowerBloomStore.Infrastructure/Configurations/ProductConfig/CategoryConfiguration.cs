using FlowerBloomStore.Domain.Entities.ProductModules;

namespace FlowerBloomStore.Infrastructure.Configurations.ProductConfig
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.ImageUrl)
                   .HasMaxLength(500);

            builder.HasIndex(c => c.Name)
                   .IsUnique();
        }
    }
}
