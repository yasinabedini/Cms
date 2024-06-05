using Cms.Endpoints.AdminPanel.Data;
using Cms.Endpoints.AdminPanel.Pages.Common;
using Cms.Endpoints.AdminPanel.Pages.News;
using Cms.Endpoints.AdminPanel.Pages.Users;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cms.Endpoints.AdminPanel.Pages.Profile
{
    #region My Profile
    public class MyProfileModel : PageModel
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly HttpClient _httpClient;


        public List<NewsViewModel> News { get; set; }
        public UserViewModel User { get; set; }

        public MyProfileModel(UserManager<CustomIdentityUser> userManager, SignInManager<CustomIdentityUser> signInManager, IHttpClientFactory factory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpClient = factory.CreateClient("AdminApi");
        }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

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

    #region Edit
    public class EditModel : PageModel
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly RoleManager<CustomIdentityRole> _roleManager;

        [BindProperty]
        public UserViewModel User { get; set; }

        [BindProperty]
        public string ReturnUrl { get; set; }


        public EditModel(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task OnGet()
        {            
            var findUser = _userManager.GetUserAsync(HttpContext.User).Result;

            User = new UserViewModel
            {
                Id = findUser.Id,
                UserName = findUser.UserName,
                Name = findUser.Name,
                PhoneNumber = findUser.PhoneNumber,
                Email = findUser.Email
            };

        }

        public async Task<IActionResult> OnPost()
        {
            var findUser = _userManager.FindByIdAsync(User.Id.ToString()).Result;
        
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

            return RedirectToPage("myprofile");
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
    
        public UserViewModel? User { get; set; }

        public SecurityModel(UserManager<CustomIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

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
            var user = await _userManager.GetUserAsync(HttpContext.User);

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

            var userModel = await _userManager.GetUserAsync(HttpContext.User);

            await _userManager.RemovePasswordAsync(userModel);
            var result = await _userManager.AddPasswordAsync(userModel, Password);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Password", "برای رمز عبور جدید به نکات بالا توجه کنید!!");
                return Page();
            }

            return RedirectToPage("MyProfile");
        }
    }
    #endregion
}
