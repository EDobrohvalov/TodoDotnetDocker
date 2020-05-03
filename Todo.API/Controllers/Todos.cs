using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.API.Models;

namespace Todo.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class TodosController : Controller
    {
        private readonly ITodoRepository _repository;

        public TodosController(ITodoRepository repository)
        {
            _repository = repository;
        }

        // GET api/todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> Get()
        {
            return new ObjectResult(await _repository.GetAllTodos());
        }

        // GET api/todos/1
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> Get(long id)
        {
            var todo = await _repository.GetTodo(id);
            if (todo == null)
                return new NotFoundResult();

            return new ObjectResult(todo);
        }

        // POST api/todos
        [HttpPost]
        public async Task<ActionResult<TodoItem>> Post([FromBody] TodoItem todoItem)
        {
            todoItem.Id = await _repository.GetNextId();
            await _repository.Create(todoItem);
            return new OkObjectResult(todoItem);
        }

        // PUT api/todos/1
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> Put(long id, [FromBody] TodoItem todoItem)
        {
            var todoFromDb = await _repository.GetTodo(id);
            if (todoFromDb == null)
                return new NotFoundResult();
            todoItem.Id = todoFromDb.Id;
            todoItem.InternalId = todoFromDb.InternalId;
            await _repository.Update(todoItem);
            return new OkObjectResult(todoItem);
        }

        // DELETE api/todos/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var post = await _repository.GetTodo(id);
            if (post == null)
                return new NotFoundResult();
            await _repository.Delete(id);
            return new OkResult();
        }
    }
}