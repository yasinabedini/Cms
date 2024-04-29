using Cms.Clients.AdminPanel.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Clients.AdminPanel.Pages.Role
{
    public class EditModel : PageModel
    {
        private readonly RoleManager<CustomIdentityRole> _roleManager;

        public EditModel(RoleManager<CustomIdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public CustomIdentityRole Role { get; set; }

        public async Task OnGet(string id)
        {
            Role = await _roleManager.FindByIdAsync(id);

            Title = Role.Name;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var findRole =await _roleManager.FindByIdAsync(Role.Id);

            findRole.Name = Title;

            var result = await _roleManager.UpdateAsync(findRole);

            if (result.Succeeded)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }

        }
    }
}
