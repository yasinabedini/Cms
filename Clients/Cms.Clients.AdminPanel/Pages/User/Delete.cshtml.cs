using Cms.Clients.AdminPanel.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Clients.AdminPanel.Pages.User
{
    public class DeleteModel : PageModel
    {
        private readonly UserManager<CustomIdentityUser> _userManager;

        public DeleteModel(UserManager<CustomIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet(string id)
        {
            var user = _userManager.Users.FirstOrDefault(t=>t.Id==id);
            await _userManager.DeleteAsync(user);

            return RedirectToPage("Index");
        }
    }
}
