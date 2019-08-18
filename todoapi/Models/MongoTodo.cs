using System;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace todos
{
  public class MongoTodo : Todo
  {
    public MongoTodo(DateTime due, string text, string id) : base(due, text, id) { }

    public static MongoTodo FactoryMethod(BsonDocument todo)
    {
      var text = todo.GetElement("text").Value.ToString();
      var due = todo.GetElement("due").Value.ToUniversalTime();
      var id = todo.GetElement("_id").Value.ToString();
      return new MongoTodo(due, text, id);
    }

    public override async Task<Todo> Save()
    {
      var todo = await TodosClient.MakeTodo(this);
      return todo;
    }
  }
}