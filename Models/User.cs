using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace musingo_auth_service.Models;

public class User
{
    [BsonId]
    [BsonElement("_id")]
    [JsonPropertyName("_id")]
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
    [BsonElement("token")]
    public string Token { get; set; }
}