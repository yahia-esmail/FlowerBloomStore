namespace FlowerBloomStore.Application.Interfaces.Roles
{
    public interface IRoleService
    {
        Task CreateRoleAsync(string roleName);
        Task<IEnumerable<string>> GetRolesAsync();
    }
}
