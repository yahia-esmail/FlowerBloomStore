namespace FlowerBloomStore.Domain.Entities.UsersModule
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
        public string? ProfilePicture { get; set; } = "\\assets\\images\\default-avatar.png";
        public DateTime? DateofBirth { get; set; }
        public Gender? Gender { get; set; }



        // Navigation Properties (العلاقات مع باقي الجداول)
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
        public virtual Cart Cart { get; set; } = new Cart();
        public virtual Wishlist Wishlist { get; set; } = new Wishlist();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        //public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        //public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();


    }
}
