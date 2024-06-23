using Cms.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Serilog;

namespace Cms.Identity;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // uncomment if you want to add a UI
        builder.Services.AddRazorPages();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"), opt => opt.MigrationsAssembly(typeof(HostingExtensions).Assembly.FullName));
        });

        builder.Services.AddIdentity<CustomIdentityUser, CustomIdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddIdentityServer(options =>
            {
                
            })                        
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients(builder.Configuration))
            .AddAspNetIdentity<CustomIdentityUser>();
            

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

        app.UseHttpsRedirection();

        // uncomment if you want to add a UI
        app.UseAuthorization();
        app.MapRazorPages();

        return app;
    }
}
