using System.Security.Claims;

namespace FlowerBloomStore.Application.Interfaces.Authentication
{
    public interface IAccountService
    {
        Task<bool> LoginAsync(LoginDto dto);
        Task LogoutAsync();
        Task<string> RegisterAsync(RegisterUserDto dto);
        Task<ApplicationUser> GetCurrentUser(ClaimsPrincipal User);

        Task UpdatePersonalInfo(ClaimsPrincipal User, ApplicationUser newUser);
    }
}
