using AutoMapper;
using MongoDB.Bson;
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


    public async Task AddNewUserAsync(User user)
    {
        var users = _mongoClient.GetDatabase("usersDb").GetCollection<User>("users");

        user.UserId = Guid.NewGuid();
        await users.InsertOneAsync(user);

    }

    public async Task<User> GetUserByLoginIdAsync(string loginId)
    {
        var users = _mongoClient.GetDatabase("usersDb").GetCollection<User>("users");

        return await (await users.FindAsync(user => user.LoginId == loginId)).FirstOrDefaultAsync();
    }

    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        var users = _mongoClient.GetDatabase("usersDb").GetCollection<User>("users");

        return await (await users.FindAsync(user => user.UserId == userId)).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        var users = _mongoClient.GetDatabase("usersDb").GetCollection<User>("users");
        
        var updateToken = Builders<User>.Update.Set(x=>x.Token, user.Token);
        var updateName = Builders<User>.Update.Set(x=>x.Token, user.Token);
        var updatePictureUrl = Builders<User>.Update.Set(x=>x.Token, user.Token);
        var updateAverageScore = Builders<User>.Update.Set(x=>x.Token, user.Token);

        var dict = new Dictionary<string, object>
        {
            {"token", user.Token},
            {"name", user.Name},
            {"pictureUrl", user.PictureUrl},
            {"averageScore", user.AverageScore}
        };

        var res = await users.UpdateOneAsync(new BsonDocument("userId", user.UserId), 
            new BsonDocument("$set", new BsonDocument(dict)), new UpdateOptions { IsUpsert = true });

        return res.ModifiedCount == 1;


    }
}