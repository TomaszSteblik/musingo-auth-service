using MediatR;
using musingo_auth_service.Dtos;

namespace musingo_auth_service.Queries;

public class GetUserQuery : IRequest<UserReadDto>
{
    public Guid UserId { get; init; }

    public GetUserQuery(Guid userId)
    {
        UserId = userId;
    }
}