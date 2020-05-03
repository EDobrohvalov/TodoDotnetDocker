using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.API.Models;

namespace Todo.API
{
    public interface ITodoRepository
    {
        // api/[GET]
        Task<IEnumerable<TodoItem>> GetAllTodos();
        // api/1/[GET]
        Task<TodoItem> GetTodo(long id);
        // api/[POST]
        Task Create(TodoItem todoItem);
        // api/[PUT]
        Task<bool> Update(TodoItem todoItem);
        // api/1/[DELETE]
        Task<bool> Delete(long id);
        Task<long> GetNextId();
    }
}