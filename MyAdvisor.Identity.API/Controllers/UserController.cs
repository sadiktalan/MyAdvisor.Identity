using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAdvisor.Identity.Application.Features.Queries;
using MyAdvisor.Identity.Application.Features.Queries.GetUserById;

namespace MyAdvisor.Identity.API.Controllers;

public class UserController : ApiControllerBase
{
    [Authorize(Policy = "User:Read")]
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDto>> GetByIdAsync(Guid userId)
    {
        return await Mediator.Send(new GetUserByIdQuery { UserId = userId });
    }
}