using MongoDB.Bson.Serialization.Attributes;

namespace musingo_auth_service.Dtos;

public class UserReadDto
{
    [BsonElement("userId")]
    public Guid UserId { get; set; }
    [BsonElement("pictureUrl")]
    public string PictureUrl { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("averageScore")]
    public double AverageScore { get; set; }
}