using AKYMicroservices.Domain.ViewModals;
using AKYMicroservices.Domain.ViewModals.Email;

namespace AKYMicroservices.Domain.Interfaces;

public interface IEmailService
{
    public Task<EmailResponseVm> SendEmailAsync(string to,string subject, string content);
}
