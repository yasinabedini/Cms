using Cms.Identity.Data;
using Cms.Identity.Models;
using Cms.Identity.Pages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Cms.Identity;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // uncomment if you want to add a UI
        builder.Services.AddRazorPages();

        builder.Services.AddAuthentication();

        builder.Services.AddDbContext<CmsIdentityDbContext>(t => t.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnectionString"))).AddIdentityCore<CustomIdentityUser>();

        builder.Services.AddIdentity<CustomIdentityUser, IdentityRole>().AddEntityFrameworkStores<CmsIdentityDbContext>();

        builder.Services.AddIdentityServer(option =>
        {
            option.EmitStaticAudienceClaim = true;
        })
           .AddInMemoryIdentityResources(Config.IdentityResources)
           .AddInMemoryApiScopes(Config.ApiScopes)
           .AddInMemoryClients(Config.Clients(builder.Configuration));
           
           

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // uncomment if you want to add a UI
        app.UseStaticFiles();
        app.UseRouting();

        app.UseIdentityServer();

        app.UseAuthentication();
        // uncomment if you want to add a UI
        app.UseAuthorization();
        app.MapRazorPages();

        return app;
    }
}
