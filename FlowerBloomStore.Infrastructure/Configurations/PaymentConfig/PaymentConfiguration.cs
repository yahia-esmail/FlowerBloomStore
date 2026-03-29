using FlowerBloomStore.Domain.Entities.PaymentModules;

namespace FlowerBloomStore.Infrastructure.Configurations.PaymentConfig
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.PaymentMethod)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(p => p.Status)
                   .IsRequired()//<----
                   .HasConversion<string>();

            builder.Property(p => p.PaidAt);

            builder.Property(p => p.TransactionId)
                   .HasMaxLength(100);

            builder.HasOne(p => p.Order)
                   .WithOne(o => o.Payment)
                   .HasForeignKey<Payment>(p => p.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.TransactionId)
                   .IsUnique()
                   .HasFilter("[TransactionId] IS NOT NULL");
        }
    }
}
