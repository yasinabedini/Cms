using Cms.Clients.AdminPanel.Data;
using Cms.Clients.AdminPanel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Clients.AdminPanel.Pages.User
{
    public class EditModel : PageModel
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly RoleManager<CustomIdentityRole> _roleManager;

        [BindProperty]
        public UpdateUserViewModel User { get; set; }

        public List<CustomIdentityRole> RoleList { get; set; }

        public List<string> UserRoles { get; set; }

        [BindProperty]
        public List<string> UserRoleSelected { get; set; }


        public EditModel(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task OnGet(string id)
        {
            var findUser = _userManager.FindByIdAsync(id).Result;

            User = new UpdateUserViewModel
            {
                Id = id,
                UserName = findUser.UserName,
                Name = findUser.Name,
                PhoneNumber = findUser.PhoneNumber,
                Email = findUser.Email
            };



            UserRoles = (await _userManager.GetRolesAsync(findUser)).ToList();
            RoleList = _roleManager.Roles.ToList();
        }

        public async Task<IActionResult> OnPost()
        {
            var findUser = _userManager.FindByIdAsync(User.Id).Result;

            UserRoles = (await _userManager.GetRolesAsync(findUser)).ToList();
            RoleList = _roleManager.Roles.ToList();

            foreach (var role in RoleList)
            {

                if (UserRoleSelected.Contains(role.Name))
                {
                    if (!(await _userManager.IsInRoleAsync(findUser, role.Name)))
                    {
                        await _userManager.AddToRoleAsync(findUser, role.Name);
                    }
                }

                else
                {
                    if (await _userManager.IsInRoleAsync(findUser, role.Name))
                    {
                        await _userManager.RemoveFromRoleAsync(findUser, role.Name);
                    }
                }

            }

            if (findUser.PhoneNumber != User.PhoneNumber)
            {
                if (_userManager.Users.Any(t => t.PhoneNumber == User.PhoneNumber))
                {
                    ModelState.AddModelError("User.PhoneNumber", "کاربری با این شماره موبایل ثبت شده است");
                }
            }

            if (findUser.UserName != User.UserName)
            {
                if (_userManager.Users.Any(t => t.UserName == User.UserName))
                {
                    ModelState.AddModelError("User.UserName", "کاربری با این نام کاربری ثبت شده است");
                }
            }

            if (!string.IsNullOrEmpty(User.Email))
            {
                if (findUser.Email != User.Email)
                {
                    if (_userManager.Users.Any(t => t.Email == User.Email))
                    {
                        ModelState.AddModelError("User.Email", "کاربری با این ایمیل ثبت شده است");
                    }
                }
            }

            findUser.UserName = User.UserName;
            findUser.Email = User.Email;
            findUser.PhoneNumber = User.PhoneNumber;
            findUser.Name = User.Name;


            var result = await _userManager.UpdateAsync(findUser);

            if (!string.IsNullOrEmpty(User.Password))
            {
                if (string.IsNullOrEmpty(User.Re_Password))
                {
                    ModelState.AddModelError("User.Re_Password", "تکرار رمز عبور را وارد کنید!");
                    return Page();
                }
                if (User.Re_Password != User.Password)
                {
                    ModelState.AddModelError("User.Password", "رمز عبور با تکرارش یکسان نیست!");
                    return Page();
                }

                await _userManager.RemovePasswordAsync(findUser);
                result = await _userManager.AddPasswordAsync(findUser, User.Password);
                if (result.Errors.Any(t => t.Description.ToLower().Contains("password")))
                {
                    ModelState.AddModelError("User.Password", "حداقل 8 حرف و شامل کاراکتر، اعداد و حرف کوچک و بزرگ");
                    return Page();
                }
            }



            return RedirectToPage("Index");
        }
    }
}
