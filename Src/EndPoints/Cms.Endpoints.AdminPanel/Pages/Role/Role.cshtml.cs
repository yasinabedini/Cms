using Cms.Endpoints.AdminPanel.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Endpoints.AdminPanel.Pages.Role
{
    public class ListModel: PageModel
    {
        private readonly RoleManager<CustomIdentityRole> _roleManager;

        public ListModel(RoleManager<CustomIdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public List<CustomIdentityRole> RoleList { get; set; }

        public void OnGet()
        {
            RoleList = _roleManager.Roles.ToList();
        }
    }

    public class CreateModel: PageModel
    {
        private readonly RoleManager<CustomIdentityRole> _roleManager;

        [BindProperty]
        public string RoleName { get; set; }

 
        public CreateModel(RoleManager<CustomIdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {            
            var result = await _roleManager.CreateAsync(new CustomIdentityRole {  Name = RoleName });

            if (result.Succeeded)
            {
                return RedirectToPage("List");
            }
            else
            {
                return Page();
            }
        }
    }
}
