namespace FlowerBloomStore.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Tables:

        //User
        public virtual DbSet<Address> Addresses { get; set; }

        //Product
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }

        //Order
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        //Shopping
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }
        public virtual DbSet<WishlistItem> WishlistItems { get; set; }

        //Payment
        public virtual DbSet<Payment> Payments { get; set; }

        //Delivery
        public virtual DbSet<Delivery> Deliveries { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }

        #endregion 

        //**************************************************//

        public AppDbContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
        }
    }
}
