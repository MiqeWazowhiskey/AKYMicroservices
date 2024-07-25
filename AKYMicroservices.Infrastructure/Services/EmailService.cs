using AKYMicroservices.Domain.Entities;
using AKYMicroservices.Domain.Interfaces;
using AKYMicroservices.Domain.Repositories;
using AKYMicroservices.Domain.ViewModals;
using AKYMicroservices.Domain.ViewModals.Email;
using AKYMicroservices.Infrastructure.Repositories;

namespace AKYMicroservices.Infrastructure.Services;

public class EmailService: IEmailService
{
    private readonly IRepository<Email, Guid> _repository;
    public EmailService(IRepository<Email, Guid> repository)
    {
        _repository = repository;
    }
    public async Task<EmailResponseVm> SendEmailAsync(EmailVm email)
    {
        try
        {
            var emailEntity = new Email
            {
                From = email.From,
                To = email.To,
                Subject = email.Subject,
                Content = email.Content,
                CreatedBy = new Guid(),
                CreatedAt = DateTime.Now
            };
            await _repository.InsertAsync(emailEntity);
            return new EmailResponseVm()
            {
                Email = new EmailVm(emailEntity),
                Status = true
            };
        }
        catch (Exception ex)
        {
            return new EmailResponseVm
            {
                Status = false,
                ErrorMessage = ex.Message
            };
        }
    }
}
