using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<IEnumerable<Models.Todo>>> Get()
        {
            return new ObjectResult(await _repository.GetAllTodos());
        }

        // GET api/todos/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Todo>> Get(long id)
        {
            var todo = await _repository.GetTodo(id);
            if (todo == null)
                return new NotFoundResult();

            return new ObjectResult(todo);
        }

        // POST api/todos
        [HttpPost]
        public async Task<ActionResult<Models.Todo>> Post([FromBody] Models.Todo todo)
        {
            todo.Id = await _repository.GetNextId();
            await _repository.Create(todo);
            return new OkObjectResult(todo);
        }

        // PUT api/todos/1
        [HttpPut("{id}")]
        public async Task<ActionResult<Models.Todo>> Put(long id, [FromBody] Models.Todo todo)
        {
            var todoFromDb = await _repository.GetTodo(id);
            if (todoFromDb == null)
                return new NotFoundResult();
            todo.Id = todoFromDb.Id;
            todo.InternalId = todoFromDb.InternalId;
            await _repository.Update(todo);
            return new OkObjectResult(todo);
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