using MediatR;
using musingo_auth_service.Dtos;

namespace musingo_auth_service.Queries;

public class GetUserByIdQuery : IRequest<UserReadDto>
{
    public Guid UserId { get; init; }

    public GetUserByIdQuery(Guid userId)
    {
        UserId = userId;
    }
}