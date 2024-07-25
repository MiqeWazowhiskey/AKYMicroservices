using AKYMicroservices.Application.Features.Commands.Email;
using AKYMicroservices.Domain.ViewModals.Email;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace AKYMicroservices.Services.Email.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EmailController : ControllerBase
{
    private readonly ISender _sender;
    
    public EmailController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpPost("Send")]
    public async Task<IActionResult> SendEmail([FromBody] PostEmailCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.Status)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}
