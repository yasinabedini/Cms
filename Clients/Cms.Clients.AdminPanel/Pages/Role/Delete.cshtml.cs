using Cms.Clients.AdminPanel.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Clients.AdminPanel.Pages.Role
{
    public class DeleteModel : PageModel
    {
        private readonly RoleManager<CustomIdentityRole> _roleManager;

        public DeleteModel(RoleManager<CustomIdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> OnGet(string id)
        {
            var role = _roleManager.Roles.FirstOrDefault(t => t.Id == id);
            await _roleManager.DeleteAsync(role);

            return RedirectToPage("Index");
        }
    }
}
