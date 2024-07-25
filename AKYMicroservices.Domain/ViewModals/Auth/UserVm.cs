using AKYMicroservices.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AKYMicroservices.Domain.ViewModals.Auth;

public class UserVm 
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public IList<string>? Roles { get; set; }
    public UserVm(){}

    public UserVm(IApplicationUser user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
        Roles = user.Roles;
    }
}
