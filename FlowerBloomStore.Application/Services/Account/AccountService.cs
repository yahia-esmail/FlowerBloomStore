using System.Security.Claims;

namespace FlowerBloomStore.Application.Services.Authentication
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> RegisterAsync(RegisterUserDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName,
                PasswordHash = dto.Password,
                PhoneNumber = dto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                throw new Exception("..فشل تسجيل الحساب الرجاء المحاولة مرة أخرى");

            await _userManager.AddToRoleAsync(user, dto.Role);

            return user.Id;
        }

        public async Task<bool> LoginAsync(LoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(
                dto.Email,
                dto.Password,
                dto.RememberMe,
                false);

            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ApplicationUser> GetCurrentUser(ClaimsPrincipal User)
            => await _userManager.GetUserAsync(User);

        public async Task UpdatePersonalInfo(ClaimsPrincipal User, ApplicationUser newUser)
        {
            var user = await _userManager.GetUserAsync(User);

            user.Email = newUser.Email;
            user.UserName = newUser.Email;
            user.FullName = newUser.FullName;
            user.PhoneNumber = newUser.PhoneNumber;
            user.ProfilePicture = newUser.ProfilePicture;
            user.Addresses = newUser.Addresses;

            var result = await _userManager.UpdateAsync(user);
        }
    }
}
