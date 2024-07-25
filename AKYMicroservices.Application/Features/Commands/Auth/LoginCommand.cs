using AKYMicroservices.Domain.Interfaces;
using AKYMicroservices.Domain.ViewModals.Auth;
using MediatR;

namespace AKYMicroservices.Application.Features.Commands.Auth;

public class LoginCommand : IRequest<LoginResultVm>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResultVm>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }
    public async Task<LoginResultVm> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
       return await _authService.LoginAsync(request.Email, request.Password);
    }
}
