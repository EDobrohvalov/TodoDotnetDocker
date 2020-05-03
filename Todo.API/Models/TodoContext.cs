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

        public IMongoCollection<TodoItem> Todos => _db.GetCollection<TodoItem>("Todos");
    }
}