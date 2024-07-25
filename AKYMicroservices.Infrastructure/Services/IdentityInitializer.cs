using AKYMicroservices.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AKYMicroservices.Infrastructure.Services;

public class IdentityInitializer : IIdentityInitializer
{
    public async Task InitializeAsync(UserManager<IApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Admin", "User" };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
        
    }
}
