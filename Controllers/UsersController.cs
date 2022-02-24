using MediatR;
using Microsoft.AspNetCore.Mvc;
using musingo_auth_service.Dtos;
using musingo_auth_service.Queries;

namespace musingo_auth_service.Controllers;

[Route("api/a/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> Login(UserLoginDto userLoginDto)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult> GetUser(Guid userId)
    {
        var query = new GetUserQuery(userId);
        var res = await _mediator.Send(query);
        return res is null ? NotFound() : Ok(res);
    }



}