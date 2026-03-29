namespace FlowerBloomStore.Application.Interfaces.User
{
    public interface IUserService
    {
        Task<ApplicationUser?> GetByIdAsync(string id);
        Task DeleteAsync(string id);
        Task<RegisterResult> RegisterAsync(RegisterUserDto registerDto);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<List<string>> GetAllRolesAsync();
        //Task<ApplicationUser?> GetCurrentUser();
    }
}
