namespace FlowerBloomStore.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _users;

        public UsersController(IUserService users)
        {
            _users = users;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //    => Ok(await _users.GetAllAsync());

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _users.DeleteAsync(id);
            return Ok();
        }
    }
}
