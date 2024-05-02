using Cms.Clients.AdminPanel.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Clients.AdminPanel.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly RoleManager<CustomIdentityRole> _roleManager;

        public List<CustomIdentityRole> Roles { get; set; }
        

        public IndexModel(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public List<CustomIdentityUser> UserList { get; set; }
        public async Task<IActionResult> OnGet(string? searchText,int selectedRole)
        {
            UserList = _userManager.Users.ToList();

            var role = await _roleManager.FindByIdAsync(selectedRole.ToString());

            if (selectedRole is not 0)
            {
                UserList = (await _userManager.GetUsersInRoleAsync(role.Name)).ToList();
            }
            if (!string.IsNullOrEmpty(searchText))
            {
                UserList = UserList.Where(t => t.UserName.Contains(searchText) || t.PhoneNumber.Contains(searchText) || t.Name.Contains(searchText)).ToList();
            }

            Roles = _roleManager.Roles.ToList();

            return Page();
        }
    }
}
