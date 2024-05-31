using Microsoft.AspNetCore.Identity;

namespace Cms.Endpoints.AdminPanel.Data
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        public string Name { get; set; }
    }
}
