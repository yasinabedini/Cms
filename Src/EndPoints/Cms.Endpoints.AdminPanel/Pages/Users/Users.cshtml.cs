using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Cms.Endpoints.AdminPanel.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Endpoints.AdminPanel.Pages.Users;

#region List
public class UsersListModel : PageModel
{
    public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    private readonly UserManager<CustomIdentityUser> _userManager;

    public UsersListModel(UserManager<CustomIdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task OnGet()
    {
        var userList = _userManager.Users.OrderByDescending(t => t.Id).ToList();

        foreach (var user in userList)
        {
            var userModel = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email ?? "",
                IsActive = !user.LockoutEnabled,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
            };

            userModel.Roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().ToList();


            Users.Add(userModel);
        }
    }
}
#endregion

#region Register
public class RegsiterModel : PageModel
{
    private readonly UserManager<CustomIdentityUser> _userManager;
    private readonly RoleManager<CustomIdentityRole> _roleManager;

    public List<CustomIdentityRole> RoleList { get; set; }

    [BindProperty]
    public RegisterViewModel User { get; set; }

    [BindProperty]
    public List<string> Roles { get; set; }

    public RegsiterModel(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager)
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

        if (User.Password != User.RePassword)
        {
            ModelState.AddModelError("User.Password", "کلمه عبور با تکرارش یکسان نیست!");
            return Page();
        }
        if (_userManager.Users.Any(t => t.UserName == User.UserName))
        {
            ModelState.AddModelError("User.UserName", "کاربری با این نام کاربری ثبت نام کرده است!");
            return Page();
        }
        if (_userManager.Users.Any(t => t.PhoneNumber == User.PhoneNumber))
        {
            ModelState.AddModelError("User.PhoneNumber", "کاربری با این شماره موبایل ثبت نام کرده است!");
            return Page();
        }

        var userModel = new CustomIdentityUser
        {
            Name = User.Name,
            Email = User?.Email,
            UserName = User.UserName,
            PhoneNumber = User.PhoneNumber,
            PhoneNumberConfirmed = true,
            EmailConfirmed = true,
            TwoFactorEnabled = false,
            NormalizedUserName = User.UserName.ToUpper(),
            NormalizedEmail = User?.Email.ToUpper(),
            LockoutEnabled = false
        };

        var result = await _userManager.CreateAsync(userModel, User.Password);

        if (result.Errors.Any(t => t.Description.ToLower().Contains("password")))
        {
            ModelState.AddModelError("User.Password", " حداقل 8 کاراکتر شامل اعدا حروف کوچک و بزرگ و کاراکتر خاص مثل @");
            return Page();
        }

        if (result.Errors.Any(t => t.Description.Contains("Username")))
        {
            ModelState.AddModelError("User.UserName", "نام کاربری فقط باید شامل حروف، اعداد و . باشد و فاصله نداشته باشد!");
            return Page();
        }

        await _userManager.AddToRolesAsync(userModel, Roles);


        return RedirectToPage("List");
    }
}
#endregion

#region Edit
public class EditModel : PageModel
{
    private readonly UserManager<CustomIdentityUser> _userManager;
    private readonly RoleManager<CustomIdentityRole> _roleManager;

    [BindProperty]
    public UserViewModel User { get; set; }

    public List<CustomIdentityRole> RoleList { get; set; }

    public List<string> UserRoles { get; set; }

    [BindProperty]
    public List<string> UserRoleSelected { get; set; }

    [BindProperty]
    public string ReturnUrl { get; set; }


    public EditModel(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task OnGet(int id,string returnUrl)
    {
        ReturnUrl = returnUrl;

        var findUser = _userManager.FindByIdAsync(id.ToString()).Result;

        User = new UserViewModel
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
        var findUser = _userManager.FindByIdAsync(User.Id.ToString()).Result;

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

        return LocalRedirect(ReturnUrl);
    }
}
#endregion

#region Details
public class DetailsModel : PageModel
{
    private readonly UserManager<CustomIdentityUser> _userManager;

    public UserViewModel User { get; set; }

    public int Id;

    public DetailsModel(UserManager<CustomIdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task OnGet(int id)
    {
        Id = id;

        var user = await _userManager.FindByIdAsync(id.ToString());

        var roles = _userManager.GetRolesAsync(user).Result.ToList();

        User = new UserViewModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user?.Email,
            IsActive = !user.LockoutEnabled,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
            Roles = roles
        };
    }
}
#endregion

#region Security
public class SecurityModel : PageModel
{
    private readonly UserManager<CustomIdentityUser> _userManager;

    [BindProperty]
    [Required(ErrorMessage = "لطفا رمز عبور را وارد کنید")]
    public string Password { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "لطفا تکرار رمز عبور را وارد کنید")]
    [Compare("Password", ErrorMessage = "رمز عبور و تکرارش یکسان نیست")]
    public string RePassword { get; set; }

    [BindProperty]
    public int? Id { get; set; }


    public UserViewModel? User { get; set; }

    public SecurityModel(UserManager<CustomIdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task OnGet(int id)
    {
        this.Id = id;

        var user = await _userManager.FindByIdAsync(id.ToString());

        var roles = _userManager.GetRolesAsync(user).Result.ToList();

        User = new UserViewModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user?.Email,
            IsActive = !user.LockoutEnabled,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
            Roles = roles
        };
    }

    public async Task<IActionResult> OnPost()
    {
        var user = await _userManager.FindByIdAsync(Id.ToString());

        var roles = _userManager.GetRolesAsync(user).Result.ToList();

        User = new UserViewModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user?.Email,
            IsActive = !user.LockoutEnabled,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
            Roles = roles
        };

        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Password != RePassword)
        {
            ModelState.AddModelError("Password", "رمز عبور و تکرارش یکسان نیست");
            return Page();
        }

        var userModel = await _userManager.FindByIdAsync(Id.ToString());

        await _userManager.RemovePasswordAsync(userModel);
        var result = await _userManager.AddPasswordAsync(userModel, Password);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("Password", "برای رمز عبور جدید به نکات بالا توجه کنید!!");
            return Page();
        }

        return RedirectToPage("Details", new { id = Id });
    }
}
#endregion

#region Change Access
public class ChangeAccessModel : PageModel
{
    private readonly UserManager<CustomIdentityUser> _userManager;

    public ChangeAccessModel(UserManager<CustomIdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGet(int id,string returnUrl)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user.LockoutEnabled)
        {
            user.LockoutEnabled = false;
        }
        else
        {
            user.LockoutEnabled = true;
        }

        await _userManager.UpdateAsync(user);

        return LocalRedirect(returnUrl);
    }
}
#endregion

