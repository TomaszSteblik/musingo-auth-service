using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MongoDB.Bson;
using Moq;
using musingo_auth_service.Data;
using musingo_auth_service.Dtos;
using musingo_auth_service.Handlers;
using musingo_auth_service.Models;
using musingo_auth_service.Profiles;
using musingo_auth_service.Queries;
using Xunit;

namespace musingo_auth_tests;

public class GetUserTestUnitTest
{
    private Mock<IUsersRepository> _usersRepositoryStub = new Mock<IUsersRepository>();
    private readonly IMapper _mapper =
        new Mapper(new MapperConfiguration(config => config.AddProfile(new UserProfile())));
    private readonly Random _random = Random.Shared;
    
    //UnitOfWork_StateUnderTest_ExpectedBehavior
    
    
    [Fact]
    public async Task GetUserByIdHandler_NoMatchingId_ReturnsNull()
    {
        //Arrange
        
        var guid = Guid.NewGuid();
        _usersRepositoryStub.Setup(repository => repository.GetUserByIdAsync(guid))!.ReturnsAsync(null as User);
        var handler = new GetUserByIdHandler(_usersRepositoryStub.Object,_mapper);
        var query = new GetUserByIdQuery(guid);
        
        //Act

        var user = await handler.Handle(query, new CancellationToken());

        //Assert

        user.Should().BeNull();
    }

    [Fact]
    public async Task GetUserByIdHandler_MatchingExists_ReturnsUserReadDto()
    {
        //Arrange
        
        var guid = Guid.NewGuid();

        var user = new User()
        {
            Id = new ObjectId(),
            Name = "Tomasz",
            Token = "asdcd",
            AverageScore = 4.4,
            LoginId = "asd",
            PictureUrl = "www.idk.net/as.jpg",
            UserId = guid
        };
        
        _usersRepositoryStub.Setup(repository => repository.GetUserByIdAsync(guid)).ReturnsAsync(user);
        var handler = new GetUserByIdHandler(_usersRepositoryStub.Object,_mapper);
        var query = new GetUserByIdQuery(guid);
        
        //Act

        var res = await handler.Handle(query, new CancellationToken());

        //Assert

        res.Should().BeEquivalentTo(_mapper.Map<UserReadDto>(user));
    }
    
    [Fact]
    public async Task GetUserByLoginIdHandler_NoMatchingId_ReturnsNull()
    {
        //Arrange

        var id = "loginId";
        _usersRepositoryStub.Setup(repository => repository.GetUserByLoginIdAsync(id))!.ReturnsAsync(null as User);
        var handler = new GetUserByLoginIdHandler(_mapper,_usersRepositoryStub.Object);
        var query = new GetUserByLoginIdQuery(id);
        
        //Act

        var user = await handler.Handle(query, new CancellationToken());

        //Assert

        user.Should().BeNull();
    }

    [Fact]
    public async Task GetUserByLoginIdHandler_MatchingExists_ReturnsUserReadDto()
    {
        //Arrange

        var id = "loginId";

        var user = new User()
        {
            Id = new ObjectId(),
            Name = "Tomasz",
            Token = "asdcd",
            AverageScore = 4.4,
            LoginId = id,
            PictureUrl = "www.idk.net/as.jpg",
            UserId = Guid.NewGuid()
        };
        
        _usersRepositoryStub.Setup(repository => repository.GetUserByLoginIdAsync(id)).ReturnsAsync(user);
        var handler = new GetUserByLoginIdHandler(_mapper, _usersRepositoryStub.Object);
        var query = new GetUserByLoginIdQuery(id);
        
        //Act

        var res = await handler.Handle(query, new CancellationToken());

        //Assert

        res.Should().BeEquivalentTo(_mapper.Map<UserReadDto>(user));
    }
}