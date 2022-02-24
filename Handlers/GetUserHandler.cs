using AutoMapper;
using MediatR;
using MongoDB.Driver;
using musingo_auth_service.Dtos;
using musingo_auth_service.Models;
using musingo_auth_service.Queries;

namespace musingo_auth_service.Handlers;

public class GetUserHandler : IRequestHandler<GetUserQuery,UserReadDto>
{
    private readonly IMapper _mapper;
    private readonly IMongoClient _mongoClient;

    public GetUserHandler(IMongoClient mongoClient, IMapper mapper)
    {
        _mongoClient = mongoClient;
        _mapper = mapper;
    }
    
    public async Task<UserReadDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        var users = _mongoClient.GetDatabase("usersDb").GetCollection<User>("users");

        var user = await (await users.FindAsync("{}")).FirstOrDefaultAsync();

        return _mapper.Map<UserReadDto>(user);
    }
}