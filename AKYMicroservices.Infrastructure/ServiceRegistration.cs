using AKYMicroservices.Domain.Interfaces;
using AKYMicroservices.Domain.Repositories;
using AKYMicroservices.Infrastructure.Data;
using AKYMicroservices.Infrastructure.Identity;
using AKYMicroservices.Infrastructure.Repositories;
using AKYMicroservices.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AKYMicroservices.Infrastructure;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IEmailService,EmailService>();
        services.AddScoped<IApplicationUser, ApplicationUser>();
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    }
}
