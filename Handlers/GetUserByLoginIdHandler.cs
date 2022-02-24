using AutoMapper;
using MediatR;
using MongoDB.Driver;
using musingo_auth_service.Dtos;
using musingo_auth_service.Models;
using musingo_auth_service.Queries;

namespace musingo_auth_service.Handlers;

public class GetUserByLoginIdHandler : IRequestHandler<GetUserByLoginIdQuery,UserReadDto>
{
    private readonly IMapper _mapper;
    private readonly IMongoClient _mongoClient;

    public GetUserByLoginIdHandler(IMongoClient mongoClient, IMapper mapper)
    {
        _mongoClient = mongoClient;
        _mapper = mapper;
    }
    
    public async Task<UserReadDto> Handle(GetUserByLoginIdQuery request, CancellationToken cancellationToken)
    {
        var loginId = request.LoginId;

        var users = _mongoClient.GetDatabase("usersDb").GetCollection<User>("users");

        var user = await (await users.FindAsync(user => user.LoginId == loginId)).FirstOrDefaultAsync();

        return _mapper.Map<UserReadDto>(user);
    }
}