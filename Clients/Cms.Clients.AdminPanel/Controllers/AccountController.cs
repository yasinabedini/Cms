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

            if (user is null)
            {
                ModelState.AddModelError("PhoneNumber", "پسورد یا شماره موبایل اشتباه است.");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, login.Password, true, false);

            if (result.Succeeded)
            {
                if (!user.PhoneNumberConfirmed)
                {
					ModelState.AddModelError("PhoneNumber", "شماره موبایل خود را تایید کنید.");
					return View();
				}

                if (!user.LockoutEnabled)
                {
                    ModelState.AddModelError("PhoneNumber", "حساب کاربری مسدود است.");
                    return View();
                }

                var props = new AuthenticationProperties();
                if (login.RemmemberMe)
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


                return LocalRedirect(login.ReturnUrl);
            }

            ModelState.AddModelError("PhoneNumber", "پسورد یا شماره موبایل اشتباه است.");
            return View();

            return View(login);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
           
            return LocalRedirect("/");
        }
    }
}
