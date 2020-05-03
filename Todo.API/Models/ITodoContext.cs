using MongoDB.Driver;

namespace Todo.API.Models
{
    public interface ITodoContext
    {
        IMongoCollection<Models.Todo> Todos { get; }
    }
}