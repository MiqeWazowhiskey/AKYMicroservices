using AKYMicroservices.Domain.Enums;

namespace AKYMicroservices.Domain.Entities;

public class BaseEntity<T>
{
    public T Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public EntityStatus Status { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
}
