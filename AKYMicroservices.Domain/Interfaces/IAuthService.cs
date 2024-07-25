using AKYMicroservices.Domain.ViewModals.Auth;

namespace AKYMicroservices.Domain.Interfaces;

public interface IAuthService
{
    public Task<LoginResultVm> LoginAsync(string email,string password);
    public Task<RegisterResultVm> RegisterAsync(string email, string password,string firstName, string lastName);
    public Task<string> GenerateJwtToken(IApplicationUser user, string role);
}
