using Cms.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cms.Identity.Data
{
    public class CmsIdentityDbContext : IdentityDbContext<CustomIdentityUser>
    {
        public CmsIdentityDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
