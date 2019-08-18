using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace todos
{
  public class TodoModel
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string text { get; set; }
    public bool done { get; set; }
    public DateTime due { get; set; }
  }
}