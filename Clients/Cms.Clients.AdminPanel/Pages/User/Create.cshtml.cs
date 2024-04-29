using Cms.Clients.AdminPanel.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.CompilerServices;

namespace Cms.Clients.AdminPanel.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly RoleManager<CustomIdentityRole> _roleManager;

        [BindProperty]
        public CustomIdentityUser User { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Re_Password { get; set; }

        public List<CustomIdentityRole> RoleList { get; set; }

        [BindProperty]
        public List<string> Roles { get; set; }

        public CreateModel(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void OnGet()
        {
            RoleList = _roleManager.Roles.ToList();
        }

        public async Task<IActionResult> OnPost()
        {
            RoleList = _roleManager.Roles.ToList();


            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Password != Re_Password)
            {
                ModelState.AddModelError("Password", "کلمه عبور با تکرارش یکسان نیست!");
                return Page();
            }
            if (_userManager.Users.Any(t=>t.UserName==User.UserName))
            {
                ModelState.AddModelError("User.UserName", "کاربری با این نام کاربری ثبت نام کرده است!");
                return Page();
            }
            if (_userManager.Users.Any(t => t.PhoneNumber == User.PhoneNumber))
            {
                ModelState.AddModelError("User.PhoneNumber", "کاربری با این شماره موبایل ثبت نام کرده است!");
                return Page();
            }
            User.PhoneNumberConfirmed = true;
            User.EmailConfirmed = true;
            User.TwoFactorEnabled = false;

            var result = await _userManager.CreateAsync(User, Password);

            if (result.Errors.Any(t=>t.Description.ToLower().Contains("password")))
            {
                ModelState.AddModelError("Password", " حداقل 8 کاراکتر شامل اعدا حروف کوچک و بزرگ و کاراکتر خاص مثل @");
                return Page();
            }
            
                await _userManager.AddToRolesAsync(User, Roles);
            

            return RedirectToPage("Index");
        }
    }
}
