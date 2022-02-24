using AutoMapper;
using musingo_auth_service.Dtos;
using musingo_auth_service.Models;

namespace musingo_auth_service.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User,UserReadDto>();
    }
}