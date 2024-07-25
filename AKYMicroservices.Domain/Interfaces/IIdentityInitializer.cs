using Microsoft.AspNetCore.Identity;

namespace AKYMicroservices.Domain.Interfaces;

public interface IIdentityInitializer
{
    public Task InitializeAsync(UserManager<IApplicationUser> userManager, RoleManager<IdentityRole> roleManager);
}
