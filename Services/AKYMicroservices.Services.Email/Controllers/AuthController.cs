using AKYMicroservices.Application.Features.Commands.Auth;
using AKYMicroservices.Domain.Interfaces;
using MediatR;

namespace AKYMicroservices.Services.Email.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController:ControllerBase
{
    private readonly ISender _sender;
    
    public AuthController(ISender sender)
    {
        _sender = sender;
    }   
    
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand request)
    {
        var result = await _sender.Send(request);
        if (!result.Status)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand request)
    {
        var result = await _sender.Send(request);
        if (!result.Status)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}
