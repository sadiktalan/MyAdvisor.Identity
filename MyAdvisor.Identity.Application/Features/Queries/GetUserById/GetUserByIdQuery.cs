using MediatR;

namespace MyAdvisor.Identity.Application.Features.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<UserDto>
{
    public Guid UserId { get; set; }
}