using MongoDB.Driver;

namespace Todo.API.Models
{
    public class TodoContext : ITodoContext
    {
        private readonly IMongoDatabase _db;

        public TodoContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }

        public IMongoCollection<Models.Todo> Todos => _db.GetCollection<Models.Todo>("Todos");
    }
}