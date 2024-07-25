using AKYMicroservices.Domain.Enums;

namespace AKYMicroservices.Domain.ViewModals;

public class BaseVm
{
    public Guid Id;
    
    public EntityStatus Status { get; set; }
    
    public DateOnly CreatedAt { get; set; }
    
    public DateOnly? UpdatedAt { get; set; }
    
    public string? UpdatedBy { get; set; }
    
    public string CreatedBy { get; set; }
}
