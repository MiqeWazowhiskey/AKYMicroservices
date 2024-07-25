using AKYMicroservices.Domain.Interfaces;
using AKYMicroservices.Domain.ViewModals.Auth;
using MediatR;

namespace AKYMicroservices.Application.Features.Commands.Auth;

public class RegisterCommand : IRequest<RegisterResultVm>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class RegisterCommandHandler: IRequestHandler<RegisterCommand,RegisterResultVm>
{
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }
    public async Task<RegisterResultVm> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authService.RegisterAsync(request.Email, request.Password,request.FirstName,request.LastName);
    }
}
