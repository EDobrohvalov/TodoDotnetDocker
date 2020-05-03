using MongoDB.Driver;

namespace Todo.API.Models
{
    public interface ITodoContext
    {
        IMongoCollection<TodoItem> Todos { get; }
    }
}