using Cms.Clients.AdminPanel.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Cms.Clients.AdminPanel.Pages.Role
{
    public class CreateModel : PageModel
    {
        private readonly RoleManager<CustomIdentityRole> _roleManager;

        [BindProperty]
        public string Name { get; set; }
        public CreateModel(RoleManager<CustomIdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {            
            var result = await _roleManager.CreateAsync(new CustomIdentityRole { Name = Name });

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
