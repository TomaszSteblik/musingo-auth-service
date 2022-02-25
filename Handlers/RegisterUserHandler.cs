using AutoMapper;
using MediatR;
using MongoDB.Driver;
using musingo_auth_service.Data;
using musingo_auth_service.Dtos;
using musingo_auth_service.Models;
using musingo_auth_service.Notifications;

namespace musingo_auth_service.Handlers;

public class RegisterUserHandler : INotificationHandler<RegisterUserNotification>
{
    private readonly IMapper _mapper;
    private readonly IUsersRepository _usersRepository;
    
    public RegisterUserHandler(IMapper mapper, IUsersRepository usersRepository)
    {
        _mapper = mapper;
        _usersRepository = usersRepository;
    }
    
    public async Task Handle(RegisterUserNotification request, CancellationToken cancellationToken)
    {

        var user = _mapper.Map<User>(request.UserLoginDto);

        await _usersRepository.AddNewUser(user);

    }
}