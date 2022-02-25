using MediatR;
using musingo_auth_service.Dtos;

namespace musingo_auth_service.Queries;

public class GetUserByLoginIdQuery : IRequest<UserReadDto>
{
    public string LoginId { get; init; }

    public GetUserByLoginIdQuery(string loginId)
    {
        LoginId = loginId;
    }
}