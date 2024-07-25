namespace AKYMicroservices.Domain.Entities;

public class Email: BaseEntity<Guid>
{
     public string From { get; set; }
     public string To { get; set; }
     public string? Subject { get; set; }
     public string? Content { get; set; }
}
