using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace todobackend.models;

public class Todo
{
    [BsonId][BsonRepresentation(BsonType.ObjectId)] 
    public string? id { get; set; }
    [BsonElement]
    public string todo_id { get; set; }
    [BsonElement]
    public string description { get; set; }
    public Todo(string Id, string Todo_id, string Description)
    {
        this.id = Id;
        this.todo_id = Todo_id;
        this.description = Description;
    }
}