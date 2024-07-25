using System.Net;
using System.Net.Mail;
using AKYMicroservices.Domain.Entities;
using AKYMicroservices.Domain.Interfaces;
using AKYMicroservices.Domain.Repositories;
using AKYMicroservices.Domain.ViewModals.Email;
using Microsoft.Extensions.Configuration;

namespace AKYMicroservices.Infrastructure.Services;

public class EmailService: IEmailService
{
    private readonly IRepository<Email, Guid> _repository;
    private readonly IConfiguration _configuration;
    public EmailService(IRepository<Email, Guid> repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }
    public async Task<EmailResponseVm> SendEmailAsync(string to,string subject, string content)
    {
        try
        {
            var mail = _configuration.GetSection("EmailSettings")["Mail"];
            var password = _configuration.GetSection("EmailSettings")["Password"];

            using (
                SmtpClient client = new SmtpClient("smtp.gmail.com", port:587)
                   {
                       EnableSsl = true,
                       UseDefaultCredentials = false,
                       Credentials = new NetworkCredential(mail, password),
                   }
                )
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(mail),
                    Subject = subject,
                    Body = content
                };
                mailMessage.To.Add(to);
                await client.SendMailAsync(mailMessage);
            }

            
            var emailEntity = new Email
            {
                From = mail,
                To = to,
                Subject = subject,
                Content = content,
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
