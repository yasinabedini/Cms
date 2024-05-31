using Microsoft.AspNetCore.Identity;

namespace Cms.Clients.AdminPanel.Data
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        public string Name { get; set; }
    }
}
