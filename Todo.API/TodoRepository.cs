using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Schema;
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
        
        public async Task<IEnumerable<Models.Todo>> GetAllTodos()
        {
            return await _context.Todos.Find(_ => true).ToListAsync();
        }

        public async Task<Models.Todo> GetTodo(long id)
        {
            var filter = Builders<Models.Todo>.Filter.Eq(m => m.Id, id);
            return await _context.Todos.Find(filter).FirstOrDefaultAsync();
        }

        public async Task Create(Models.Todo todo)
        {
            await _context.Todos.InsertOneAsync(todo);
        }

        public async Task<bool> Update(Models.Todo todo)
        {
            var updateResult = await _context.Todos.ReplaceOneAsync(g => g.Id == todo.Id, todo);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(long id)
        {
            var filter = Builders<Models.Todo>.Filter.Eq(m => m.Id, id);
            var deleteResult = await _context.Todos.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<long> GetNextId()
        {
            return await _context.Todos.CountDocumentsAsync(new BsonDocument()) + 1;
        }
    }
}