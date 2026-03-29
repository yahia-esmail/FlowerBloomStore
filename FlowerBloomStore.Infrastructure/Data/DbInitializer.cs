using Microsoft.AspNetCore.Identity;

namespace FlowerBloomStore.Infrastructure.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await context.Database.EnsureCreatedAsync();

            // إنشاء أدوار
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("Customer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }
            if (!await roleManager.RoleExistsAsync("Vendor"))
            {
                await roleManager.CreateAsync(new IdentityRole("Vendor"));
            }

            // إنشاء أدمن افتراضي
            var adminEmail = "admin@flowerstore.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new ApplicationUser { UserName = adminEmail, Email = adminEmail, FullName = "Admin User" };
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
