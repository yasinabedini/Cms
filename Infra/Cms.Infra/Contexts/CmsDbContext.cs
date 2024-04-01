using Cms.Domain.Common.ValueObjects;
using Cms.Domain.Models.Language.Entities;
using Cms.Domain.Models.Sweeper.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Infra.Contexts
{
    public class CmsDbContext : IdentityDbContext
    {
        public CmsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Sweeper> Sweepers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore(typeof(BusinessId));

            builder.ApplyConfigurationsFromAssembly(typeof(CmsDbContext).Assembly);

            base.OnModelCreating(builder);
        }
    }
}
