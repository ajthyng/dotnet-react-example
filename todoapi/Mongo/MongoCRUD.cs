using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace todos
{
static public class MongoCRUD
{
  private static IMongoDatabase db;

  static MongoCRUD()
  {
    var client = new MongoClient("mongodb://mongo:27017");
    db = client.GetDatabase("todoapp");
  }

  static async public Task InsertRecord<T>(string name, T record)
  {
    var collection = db.GetCollection<T>(name);
    await collection.InsertOneAsync(record);
  }

  static public async Task<List<T>> LoadRecords<T>(string name)
  {
    var collection = db.GetCollection<T>(name);
    var records = await collection.Find(new BsonDocument()).ToListAsync();
    return records;
  }

  static public async Task<T> LoadRecordById<T>(string name, ObjectId id)
  {
    var collection = db.GetCollection<T>(name);
    var filter = Builders<T>.Filter.Eq("Id", id);

    var record = await collection.Find(filter).FirstAsync();
    return record;
  }

  static public async Task UpdateRecord<T>(string name, string id, UpdateDefinition<T> updateDefinition)
  {
    var collection = db.GetCollection<T>(name);
    await collection.FindOneAndUpdateAsync(new BsonDocument("_id", new ObjectId(id)), updateDefinition);
  }
}
}