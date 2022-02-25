using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using musingo_auth_service.Commands;
using musingo_auth_service.Data;
using musingo_auth_service.Handlers;
using musingo_auth_service.Models;
using musingo_auth_service.Profiles;
using Xunit;

namespace musingo_auth_tests;

public class RefreshTokenUnitTest
{
    private Mock<IUsersRepository> _usersRepositoryStub = new Mock<IUsersRepository>();

    //UnitOfWork_StateUnderTest_ExpectedBehavior

    [Fact]
    public async Task RefreshTokenHandler_UserExist_ReturnsNewToken()
    {
        //Arrange
        var oldToken = "asddd";
        
        var user = new User()
        {
            Token = oldToken,
            UserId = Guid.NewGuid(),
            LoginId = "asdd"
        };

        _usersRepositoryStub.Setup(repository => repository.UpdateUserAsync(user))!.ReturnsAsync(true);
        _usersRepositoryStub.Setup(repository => repository.GetUserByLoginIdAsync(user.LoginId)).ReturnsAsync(user);
        var handler = new RefreshUserTokenHandler(_usersRepositoryStub.Object);
        var command = new RefreshUserTokenCommand(user.LoginId);
        
        //Act

        var token = await handler.Handle(command,new CancellationToken());
        
        //Assert
        
        token.Should().NotBeEquivalentTo(oldToken);
        token.Should().BeEquivalentTo(user.Token);
    }
    
}