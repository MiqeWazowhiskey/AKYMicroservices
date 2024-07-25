using AKYMicroservices.Domain.Interfaces;
using AKYMicroservices.Domain.ViewModals.Email;
using MediatR;

namespace AKYMicroservices.Application.Features.Commands.Email;

public class PostEmailCommand : IRequest<EmailResponseVm>
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
}

public class PostEmailCommandHandler : IRequestHandler<PostEmailCommand, EmailResponseVm>
{
    private readonly IEmailService _emailService;
    public PostEmailCommandHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }
    public async Task<EmailResponseVm> Handle(PostEmailCommand request, CancellationToken cancellationToken)
    {
        return await _emailService.SendEmailAsync(request.To,request.Subject, request.Content);
    }
}
