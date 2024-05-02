using Cms.Clients.AdminPanel.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Clients.AdminPanel.Pages.User
{
    public class DeleteModel : PageModel
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly RoleManager<CustomIdentityRole> _roleManager;

        [BindProperty]
        public CustomIdentityUser User { get; set; }
        public List<CustomIdentityRole> RoleList { get; set; }
        public List<string> UserRoles { get; set; }


        public DeleteModel(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task OnGet(string id)
        {
            User = _userManager.Users.FirstOrDefault(t => t.Id == id);

            UserRoles = (await _userManager.GetRolesAsync(User)).ToList();
            RoleList = _roleManager.Roles.ToList();
        }

        public async Task<IActionResult> OnPost()
        {
            var findUser = _userManager.FindByIdAsync(User.Id).Result;
            
            await _userManager.DeleteAsync(findUser);

            return RedirectToPage("Index");
        }
    }
}
