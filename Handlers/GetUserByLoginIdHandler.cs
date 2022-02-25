using AutoMapper;
using MediatR;
using MongoDB.Driver;
using musingo_auth_service.Data;
using musingo_auth_service.Dtos;
using musingo_auth_service.Models;
using musingo_auth_service.Queries;

namespace musingo_auth_service.Handlers;

public class GetUserByLoginIdHandler : IRequestHandler<GetUserByLoginIdQuery,UserReadDto>
{
    private readonly IMapper _mapper;
    private readonly IUsersRepository _usersRepository;

    public GetUserByLoginIdHandler(IMapper mapper, IUsersRepository usersRepository)
    {
        _mapper = mapper;
        _usersRepository = usersRepository;
    }
    
    public async Task<UserReadDto> Handle(GetUserByLoginIdQuery request, CancellationToken cancellationToken)
    {
        var loginId = request.LoginId;

        var user = await _usersRepository.GetUserByLoginId(loginId);

        return _mapper.Map<UserReadDto>(user);
    }
}