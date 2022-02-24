using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace musingo_auth_service.Models;

public class User
{
    [BsonIgnore]
    [BsonId]
    [BsonElement("_id")]
    public ObjectId Id { get; set; }
    [BsonElement("userId")]
    public Guid UserId { get; set; }
    [BsonElement("loginId")]
    public string LoginId { get; set; }
    [BsonElement("pictureUrl")]
    public string PictureUrl { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("averageScore")]
    public double AverageScore { get; set; }
}