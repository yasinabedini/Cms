using Cms.Clients.AdminPanel.Data;
using Cms.Clients.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cms.Clients.AdminPanel.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly UserManager<CustomIdentityUser> _userManager;

        public AccountController(SignInManager<CustomIdentityUser> signInManager, UserManager<CustomIdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("/Account/Login")]
        public IActionResult Login(string? ReturnUrl)
        {
            LoginViewModel login = new LoginViewModel() { ReturnUrl = ReturnUrl };

            return View(login);
        }

        [HttpPost]
        [Route("/Account/Login")]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            CustomIdentityUser user = _userManager.Users.FirstOrDefault(t => t.PhoneNumber == login.PhoneNumber);

            var result = await _signInManager.PasswordSignInAsync(user, login.Password, true, false);

            if (result.Succeeded)
            {

                var props = new AuthenticationProperties();
                if (true)
                {
                    props.IsPersistent = true;
                    props.ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30));
                };

                var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name,user.NormalizedUserName),
                    };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);


                return LocalRedirect("/");
            }

            ModelState.AddModelError(string.Empty, "پسورد یا نام کاربری اشتباه است.");
            return View();

            return View(login);
        }

    }
}
