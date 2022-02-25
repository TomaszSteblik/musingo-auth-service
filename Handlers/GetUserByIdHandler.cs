using AutoMapper;
using MediatR;
using MongoDB.Driver;
using musingo_auth_service.Data;
using musingo_auth_service.Dtos;
using musingo_auth_service.Models;
using musingo_auth_service.Queries;

namespace musingo_auth_service.Handlers;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery,UserReadDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    
    public async Task<UserReadDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        var user = await _usersRepository.GetUserById(userId);

        return _mapper.Map<UserReadDto>(user);
    }
}