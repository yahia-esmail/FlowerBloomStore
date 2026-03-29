
using FlowerBloomStore.Application.Interfaces.Orders;

using FlowerBloomStore.Application.Interfaces.Products;
using FlowerBloomStore.Application.Services;
using FlowerBloomStore.Application.Services.Authentication;
using FlowerBloomStore.Application.Services.Orders;
using FlowerBloomStore.Application.Services.Products;
using FlowerBloomStore.Application.Services.User;
using FlowerBloomStore.Domain.Entities.ShoppingModules;
using FlowerBloomStore.Domain.Entities.UsersModule;
using FlowerBloomStore.Domain.Interfaces;
using FlowerBloomStore.Domain.Interfaces.CategoryRepo;
using FlowerBloomStore.Domain.Interfaces.GenericRepo;
using FlowerBloomStore.Domain.Interfaces.OrderRepo;
using FlowerBloomStore.Domain.Interfaces.ProductRepo;
using FlowerBloomStore.Domain.Interfaces.ShoppingRepo;
using FlowerBloomStore.Infrastructure.Data;
using FlowerBloomStore.Infrastructure.Repositories;
using FlowerBloomStore.Infrastructure.Repositories.GenericRepository;
using FlowerBloomStore.Infrastructure.Repositories.OrderRepo;
using FlowerBloomStore.Infrastructure.Repositories.ProductRepo;
using FlowerBloomStore.Infrastructure.Repositories.ProductRepository.ProductRepo;

using FlowerBloomStore.Infrastructure.Repositories.ShoppingRepo;
using FlowerBloomStore.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlowerBloomStore.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Services
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages();

            // Add Services to the AppDbContext:
            builder.Services
                   .AddDbContext<AppDbContext>(options =>
                       options.UseLazyLoadingProxies()
                              .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                   );

            // Setup Identity:
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // إعدادات كلمة المرور
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

                // إعدادات المستخدم
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false; // غيرها حسب احتياجك
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();




            // Dependency Injections:
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            
             builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();

            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<ICartService, CartService>();


            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
            builder.Services.AddScoped<IWishlistItemRepository, WishlistItemRepository>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<ILogger, Logger<AccountController>>();
            #endregion



            var app = builder.Build();

            #region Middelwares
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication(); 
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapStaticAssets();
            app.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            #endregion

            // تهيئة الأدوار والمستخدم الافتراضي (اختياري)
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppDbContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await DbInitializer.InitializeAsync(context, userManager, roleManager);
            }


            app.Run();
        }
    }
}
