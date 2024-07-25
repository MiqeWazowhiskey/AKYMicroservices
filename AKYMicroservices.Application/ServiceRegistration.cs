using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AKYMicroservices.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

    }
}
