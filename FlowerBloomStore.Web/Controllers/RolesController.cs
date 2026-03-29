namespace FlowerBloomStore.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private readonly IRoleService _roles;

        public RolesController(IRoleService roles)
        {
            _roles = roles;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string role)
        {
            await _roles.CreateRoleAsync(role);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _roles.GetRolesAsync());
    }
}
