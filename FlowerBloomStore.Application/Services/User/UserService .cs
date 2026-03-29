namespace FlowerBloomStore.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
            => _userManager.Users.ToList();

        public async Task<ApplicationUser?> GetByIdAsync(string id)
            => await _userManager.FindByIdAsync(id);

        public async Task DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
                await _userManager.DeleteAsync(user);
        }

        public async Task<RegisterResult> RegisterAsync(RegisterUserDto registerDto)
        {
            try
            {
                // التحقق من وجود الأدوار وإنشائها إذا لم توجد
                await EnsureRolesExistAsync();

                // إنشاء مستخدم جديد
                var user = new ApplicationUser
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                    FullName = registerDto.FullName,
                    CreatedAt = DateTime.UtcNow,
                    EmailConfirmed = true // يمكن تغييرها حسب احتياجك
                };

                // محاولة إنشاء المستخدم
                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (result.Succeeded)
                {
                    // إضافة المستخدم للدور المحدد
                    await _userManager.AddToRoleAsync(user, registerDto.Role);

                    // تسجيل الدخول تلقائياً بعد التسجيل (اختياري)
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return new RegisterResult
                    {
                        Succeeded = true,
                        UserId = user.Id
                    };
                }

                return new RegisterResult
                {
                    Succeeded = false,
                    Errors = result.Errors.Select(e => e.Description).ToArray()
                };
            }
            catch (Exception ex)
            {
                return new RegisterResult
                {
                    Succeeded = false,
                    Errors = new[] { "حدث خطأ أثناء التسجيل: " + ex.Message }
                };
            }
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        public async Task<List<string>> GetAllRolesAsync()
        {
            throw new NotImplementedException();
            //return await _roleManager.Roles.Select(r => r.Name).ToListAsync();
        }

        private async Task EnsureRolesExistAsync()
        {
            string[] roles = { "Customer", "Admin" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        //public async Task<ApplicationUser?> GetCurrentUser()
        //    => _userManager.GetUserAsync(User);

    }
}

