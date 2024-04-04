using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement.API.Features.Log;

[Route("log/[controller]/[action]")]
[ApiController]
public class LogController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Index([FromServices] IMediator mediator, [FromQuery] Index.Query query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }
}
