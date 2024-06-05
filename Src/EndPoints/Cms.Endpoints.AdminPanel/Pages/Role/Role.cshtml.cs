using Cms.Endpoints.AdminPanel.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Endpoints.AdminPanel.Pages.Role
{
    #region List
    public class ListModel : PageModel
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
    #endregion

    #region Create
    public class CreateModel : PageModel
    {
        private readonly RoleManager<CustomIdentityRole> _roleManager;

        [BindProperty]
        public string RoleName { get; set; }


        public CreateModel(RoleManager<CustomIdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

   
        public async Task<IActionResult> OnPost()
        {
            var result = await _roleManager.CreateAsync(new CustomIdentityRole { Name = RoleName });

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
    #endregion

    #region Edit
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
            var findRole = await _roleManager.FindByIdAsync(Role.Id.ToString());

            findRole.Name = Title;

            var result = await _roleManager.UpdateAsync(findRole);

            if (result.Succeeded)
            {
                return RedirectToPage("list");
            }
            else
            {
                return Page();
            }

        }
    }
    #endregion

    #region Delete
    public class DeleteModel : PageModel
    {
        private readonly RoleManager<CustomIdentityRole> _roleManager;

        public DeleteModel(RoleManager<CustomIdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            var role = _roleManager.Roles.FirstOrDefault(t => t.Id == id);
            await _roleManager.DeleteAsync(role);

            return RedirectToPage("list");
        }
    }
    #endregion
}
