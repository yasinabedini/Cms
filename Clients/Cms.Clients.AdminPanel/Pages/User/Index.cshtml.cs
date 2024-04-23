using Cms.Clients.AdminPanel.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Clients.AdminPanel.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<CustomIdentityUser> _userManager;

        public IndexModel(UserManager<CustomIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public List<CustomIdentityUser> UserList { get; set; }
        public void OnGet(int pageNumber)
        {
            UserList = _userManager.Users.ToList();
        }
    }
}
