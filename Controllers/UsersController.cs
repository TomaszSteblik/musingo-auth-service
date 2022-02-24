using MediatR;
using Microsoft.AspNetCore.Mvc;
using musingo_auth_service.Dtos;
using musingo_auth_service.Notifications;
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
        var query = new GetUserByLoginIdQuery(userLoginDto.LoginId);
        var res = await _mediator.Send(query);

        if (res is null)
        {
            var notification = new RegisterUserNotification(userLoginDto);
            await _mediator.Publish(notification);
        }

        return Ok(await _mediator.Send(query));

    }

    [HttpGet("{userId}")]
    public async Task<ActionResult> GetUser(Guid userId)
    {
        var query = new GetUserByIdQuery(userId);
        var res = await _mediator.Send(query);
        return res is null ? NotFound() : Ok(res);
    }



}