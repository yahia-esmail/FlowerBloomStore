namespace FlowerBloomStore.Application.Services.Roles
{
    internal class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task CreateRoleAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(
                    new IdentityRole(roleName));
        }

        public async Task<IEnumerable<string>> GetRolesAsync()
            => _roleManager.Roles.Select(r => r.Name)!;
    }
}
