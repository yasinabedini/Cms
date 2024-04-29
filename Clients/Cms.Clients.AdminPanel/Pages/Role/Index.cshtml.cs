using Cms.Clients.AdminPanel.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Clients.AdminPanel.Pages.Role
{
    public class IndexModel : PageModel
    {
        private readonly RoleManager<CustomIdentityRole> _roleManager;

        public IndexModel(RoleManager<CustomIdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public List<CustomIdentityRole> RoleList { get; set; }

        public void OnGet()
        {
            RoleList = _roleManager.Roles.ToList();          
        }
    }
}
