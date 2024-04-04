using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement.API.Features.User;

[Route("user/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromServices] IMediator mediator, [FromBody] Create.Command command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromServices] IMediator mediator, [FromBody] Delete.Query query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromServices] IMediator mediator, [FromBody] Edit.Command command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromServices] IMediator mediator, [FromQuery] Index.Query query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }
}
