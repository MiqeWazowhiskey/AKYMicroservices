using AKYMicroservices.Domain.Enums;
using Microsoft.AspNetCore.Identity;


namespace AKYMicroservices.Domain.Interfaces;

public class IApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IList<string> Roles { get; set; }
    public EntityStatus Status { get; set; }
    public DateOnly CreatedAt { get; set; }
    public DateOnly? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public string CreatedBy { get; set; }
}
