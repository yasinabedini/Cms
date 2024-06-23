using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Site;

public static class DependencyInjection
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
