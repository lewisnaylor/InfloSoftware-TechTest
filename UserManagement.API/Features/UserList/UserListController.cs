using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement.API.Features.UserList;

[Route("userList/[controller]")]
[ApiController]
public class UserListController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Index([FromServices] IMediator mediator, [FromQuery] Index.Query query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }
}
