using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Todo
{
    [BsonElement("_id")]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    [BsonElement("title")]
    public string Title { get; set; } = null!;
}