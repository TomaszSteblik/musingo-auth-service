using AutoMapper;
using MediatR;
using MongoDB.Driver;
using musingo_auth_service.Dtos;
using musingo_auth_service.Models;
using musingo_auth_service.Notifications;

namespace musingo_auth_service.Handlers;

public class RegisterUserHandler : INotificationHandler<RegisterUserNotification>
{
    private readonly IMapper _mapper;
    private readonly IMongoClient _mongoClient;
    
    public RegisterUserHandler(IMapper mapper, IMongoClient mongoClient)
    {
        _mapper = mapper;
        _mongoClient = mongoClient;
    }
    
    public async Task Handle(RegisterUserNotification request, CancellationToken cancellationToken)
    {

        var users = _mongoClient.GetDatabase("usersDb").GetCollection<User>("users");

        var user = _mapper.Map<User>(request.UserLoginDto);
        user.UserId = Guid.NewGuid();

        await users.InsertOneAsync(user, cancellationToken: cancellationToken);
    }
}