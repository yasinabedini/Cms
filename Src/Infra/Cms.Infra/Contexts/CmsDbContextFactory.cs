using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Contexts
{
    public class CmsDbContextFactory : IDesignTimeDbContextFactory<CmsDbContext>
    {
        public CmsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CmsDbContext>();
            optionsBuilder.UseSqlServer("Server=27-SRV-TAXIPLUS;Database=Cms-Db;User Id=sa;Password=Aa123456;TrustServerCertificate=True");

            return new CmsDbContext(optionsBuilder.Options);
        }
    }
}
