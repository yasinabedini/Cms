using Cms.Domain;
using Cms.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cms.Infra.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using System.Runtime.CompilerServices;

namespace Cms.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDomain();

        services.AddDbContext<CmsDbContext>(c=>c.UseSqlServer("Server=YasiAbdn\\ABDN;Database=Cms-Db;Integrated Security=true;TrustServerCertificate=True"));
                        
        return services;
    }
}
