using AutoMapper;
using MongoDB.Driver;
using musingo_auth_service.Models;

namespace musingo_auth_service.Data;

public class UsersRepository : IUsersRepository
{
    private readonly IMongoClient _mongoClient;
    
    public UsersRepository(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }


    public async Task AddNewUser(User user)
    {
        var users = _mongoClient.GetDatabase("usersDb").GetCollection<User>("users");

        user.UserId = Guid.NewGuid();
        await users.InsertOneAsync(user);

    }

    public async Task<User> GetUserByLoginId(string loginId)
    {
        var users = _mongoClient.GetDatabase("usersDb").GetCollection<User>("users");

        return await (await users.FindAsync(user => user.LoginId == loginId)).FirstOrDefaultAsync();
    }

    public async Task<User> GetUserById(Guid userId)
    {
        var users = _mongoClient.GetDatabase("usersDb").GetCollection<User>("users");

        return await (await users.FindAsync(user => user.UserId == userId)).FirstOrDefaultAsync();
    }
}