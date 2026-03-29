using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;

namespace FlowerBloomStore.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _auth;
        private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(IAccountService auth, ILogger logger, UserManager<ApplicationUser> userManager)
        {
            _auth = auth;
            _logger = logger;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult Register() => View("Register");

        [HttpGet]
        public IActionResult Login() => View("Login");
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            var dto = new RegisterUserDto
            {
                FullName = model.FullName,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role,
                PhoneNumber = model.PhoneNumber
            };
            string id = await _auth.RegisterAsync(dto);

            TempData["SuccessMessage"] = "تم إنشاء الحساب بنجاح!";
            return RedirectToAction("Login", "FoundIt");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var loginDTO = new LoginDto
            {
                Email = model.Email,
                Password = model.Password,
                RememberMe = model.RememberMe
            };
            var success = await _auth.LoginAsync(loginDTO);
            if (!success)
            {
                ModelState.AddModelError("", "اسم المستخدم أو كلمة المرور غير صحيحة");
                return View("Login", model);
            }
            return RedirectToAction("Index","Home");
        }

        [Authorize]
        public async Task<IActionResult> Profile(string tab = "personal-info")
        {
            try
            {
                var user = await _auth.GetCurrentUser(User);
                var profile = new ProfileViewModel
                {
                    FirstName = user.FullName.Split(' ').First(),
                    LastName = string.Join(" ", user.FullName.Split(' ').Skip(1)),
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    ProfileImage = user.ProfilePicture,
                    MemberSince = user.CreatedAt,
                    IsPremiumMember = true,
                    TotalOrders = user.Orders.Where(o => o.Status == Domain.Enums.OrderStatus.Delivered).ToList().Count(),
                    FavoriteCount = user.Wishlist.Items.Count(),
                    DateOfBirth = user.DateofBirth,
                    PreferredLanguage = "العربية",
                    ReceiveEmailNotifications = true,
                    ReceiveSmsNotifications = true,
                    SubscribeToNewsletter = true,
                    TwoFactorEnabled = false,
                    LastPasswordChange = DateTime.Now.AddMonths(-2),
                    SavedAddresses = user.Addresses.ToList()
                };

                ViewBag.CurrentTab = tab;
                _logger.LogInformation("Profile page accessed");

                return View(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading profile");
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdatePersonalInfo(ProfileViewModel model)
        {
            var newUser = new ApplicationUser
            {
                Email = model.Email,
                FullName = model.FirstName + " " + model.LastName,
                PhoneNumber = model.PhoneNumber,
                Addresses = model.SavedAddresses,
                ProfilePicture = model.ProfileImage
                //CreatedAt = model.MemberSince,
                //UserName = model.Email,
                //DateofBirth = model.DateOfBirth,
                
            };
            await _auth.UpdatePersonalInfo(User, newUser);
            
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _auth.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
