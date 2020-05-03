using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.API
{
    public interface ITodoRepository
    {
        // api/[GET]
        Task<IEnumerable<Models.Todo>> GetAllTodos();
        // api/1/[GET]
        Task<Models.Todo> GetTodo(long id);
        // api/[POST]
        Task Create(Models.Todo todo);
        // api/[PUT]
        Task<bool> Update(Models.Todo todo);
        // api/1/[DELETE]
        Task<bool> Delete(long id);
        Task<long> GetNextId();
    }
}