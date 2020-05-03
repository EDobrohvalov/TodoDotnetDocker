using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Todo.API.Models;

namespace Todo.API
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ITodoContext _context;

        public TodoRepository(ITodoContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<TodoItem>> GetAllTodos()
        {
            return await _context.Todos.Find(_ => true).ToListAsync();
        }

        public async Task<TodoItem> GetTodo(long id)
        {
            var filter = Builders<TodoItem>.Filter.Eq(m => m.Id, id);
            return await _context.Todos.Find(filter).FirstOrDefaultAsync();
        }

        public async Task Create(TodoItem todoItem)
        {
            await _context.Todos.InsertOneAsync(todoItem);
        }

        public async Task<bool> Update(TodoItem todoItem)
        {
            var updateResult = await _context.Todos.ReplaceOneAsync(g => g.Id == todoItem.Id, todoItem);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(long id)
        {
            var filter = Builders<TodoItem>.Filter.Eq(m => m.Id, id);
            var deleteResult = await _context.Todos.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<long> GetNextId()
        {
            return await _context.Todos.CountDocumentsAsync(new BsonDocument()) + 1;
        }
    }
}