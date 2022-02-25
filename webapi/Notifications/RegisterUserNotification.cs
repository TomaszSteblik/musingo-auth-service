using MediatR;
using musingo_auth_service.Dtos;

namespace musingo_auth_service.Notifications;

public class RegisterUserNotification : INotification
{
    public UserLoginDto UserLoginDto { get; set; }
    
    public RegisterUserNotification(UserLoginDto userLoginDto)
    {
        UserLoginDto = userLoginDto;
    }
}