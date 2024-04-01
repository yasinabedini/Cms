using Cms.Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cmd.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddInfrastructure();

        services.AddMediatR(configuration =>
        configuration.RegisterServicesFromAssemblies(assembly));

        return services;
    }
}
