using Microsoft.EntityFrameworkCore;



namespace Cms.Endpoints.Admin;

public static class DependencyInjection
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();
        
        return services;
    }
}
