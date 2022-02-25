using MediatR;

namespace musingo_auth_service.Commands;

public class RefreshUserTokenCommand : IRequest<string>
{
    public string LoginId { get; set; }
    public RefreshUserTokenCommand(string loginId)
    {
        LoginId = loginId;
    }
}