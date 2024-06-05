using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using Cms.Endpoints.AdminPanel.Data;
using Cms.Endpoints.AdminPanel.Pages.Common;
using Cms.Endpoints.AdminPanel.Pages.News;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Cms.Endpoints.AdminPanel.Pages.Users;

#region List
public class UsersListModel : PageModel
{
    public PagedData<UserViewModel> Users { get; set; } = new PagedData<UserViewModel>();
    private readonly UserManager<CustomIdentityUser> _userManager;
    private readonly RoleManager<CustomIdentityRole> _roleManager;
    public List<CustomIdentityRole> Roles { get; set; }

    public UsersListModel(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task OnGet(string searchText = "", string role = "", int statusId = 0, int pageNumber = 1)
    {
        var userList = _userManager.Users.OrderByDescending(t => t.Id).ToList();
        Roles = _roleManager.Roles.ToList();
        Users.QueryResult = new List<UserViewModel>();

        foreach (var user in userList)
        {
            var userModel = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email ?? "",
                LockoutEnabled = !user.LockoutEnabled,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
            };

            userModel.Roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().ToList();

            Users.QueryResult.Add(userModel);
        }

        if (!string.IsNullOrEmpty(searchText))
        {
            Users.QueryResult = Users.QueryResult.Where(t => t.Name.Contains(searchText) || t.UserName.Contains(searchText) || t.Email.Contains(searchText) || t.PhoneNumber.Contains(searchText)).ToList();
        }

        if (!string.IsNullOrEmpty(role))
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(role);

            Users.QueryResult = new List<UserViewModel>();

            foreach (var user in usersInRole)
            {
                var userModel = new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email ?? "",
                    LockoutEnabled = !user.LockoutEnabled,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                };

                userModel.Roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().ToList();

                Users.QueryResult.Add(userModel);
            }
        }

        if (statusId != 0)
        {
            if (statusId == 1)
            {
                Users.QueryResult = Users.QueryResult.Where(t => !t.LockoutEnabled).ToList();
            }
            else
            {
                Users.QueryResult = Users.QueryResult.Where(t => t.LockoutEnabled).ToList();
            }
        }

        ViewData["totalUser"] = Users.QueryResult.Count;
        ViewData["activeUser"] = Users.QueryResult.Count(t=>!t.LockoutEnabled);
        ViewData["notActiveUser"] = Users.QueryResult.Count(t=>t.LockoutEnabled);

        int pageSize = 25;
        Users.PageNumber = pageNumber;
        Users.PageSize = pageSize;
        Users.TotalCount = Users.QueryResult.Count;
        Users.QueryResult = Users.QueryResult.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
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

    public async Task OnGet(int id, string returnUrl)
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
    private readonly HttpClient _httpClient;


    public List<NewsViewModel> News { get; set; }
    public UserViewModel User { get; set; }

    public int Id;

    public DetailsModel(UserManager<CustomIdentityUser> userManager, IHttpClientFactory factory)
    {
        _userManager = userManager;
        _httpClient = factory.CreateClient("AdminApi");
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
            LockoutEnabled = !user.LockoutEnabled,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
            Roles = roles
        };

        var data = new { pageNumber = 1, pageSize = 200, typeId = 0, languageId = 0, isPage = false }; // Your data object
        var jsonInString = JsonConvert.SerializeObject(data);
        var content = new StringContent(jsonInString, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("/api/News/GetAll", content);

        var result = await response.Content.ReadAsStringAsync();

        News = JsonConvert.DeserializeObject<PagedData<NewsViewModel>>(result).QueryResult;

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
            LockoutEnabled = !user.LockoutEnabled,
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
            LockoutEnabled = !user.LockoutEnabled,
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

    public async Task<IActionResult> OnGet(int id, string returnUrl)
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

#region Delete
public class DeleteModel : PageModel
{
    private readonly UserManager<CustomIdentityUser> _userManager;

    [BindProperty]
    public CustomIdentityUser User { get; set; }


    public DeleteModel(UserManager<CustomIdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task OnGet(int id)
    {
        var findUser = _userManager.Users.FirstOrDefault(t => t.Id == id);

        await _userManager.DeleteAsync(findUser);
    }
}
#endregion

