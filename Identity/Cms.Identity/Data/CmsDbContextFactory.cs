using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Identity.Data
{
    public class CmsDbContextFactory : IDesignTimeDbContextFactory<CmsIdentityDbContext>
    {
        public CmsIdentityDbContext CreateDbContext(string[] args)
        {            
            var optionsBuilder = new DbContextOptionsBuilder<CmsIdentityDbContext>();
            optionsBuilder.UseSqlServer("Server=YasiAbdn\\ABDN;Database=CmsIdentity-Db;Integrated Security=true;TrustServerCertificate=True");

            return new CmsIdentityDbContext(optionsBuilder.Options);
        }
    }
}
