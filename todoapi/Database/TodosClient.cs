using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace todos
{
  static class TodosClient
  {
    static public MongoClient MongoDB { get; private set; }
    static public IMongoDatabase Database
    {
      get
      {
        return MongoDB.GetDatabase("todoapp");
      }
    }

    static TodosClient()
    {
      MongoDB = new MongoClient("mongodb://mongo:27017");
    }

    public static async Task<IEnumerable<Todo>> GetTodos()
    {
      var todosCollection = Database.GetCollection<BsonDocument>("todos");
      var todos = await todosCollection.Find(new BsonDocument()).ToListAsync();
      var results = new List<MongoTodo>();

      foreach (BsonDocument todo in todos)
      {
        results.Add(MongoTodo.FactoryMethod(todo));
      }
      return results;
    }

    async public static Task<Todo> MakeTodo(Todo todo)
    {
      var document = new BsonDocument
      {
        { "due", todo.DueDate },
        { "text", todo.Text }
      };

      await Database.GetCollection<BsonDocument>("todos").InsertOneAsync(document);
      return todo;
    }
  }
}
