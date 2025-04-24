using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo.Domain.Models;
using Todo.Domain.Repositories;

namespace Todo.Web.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IUserRepository _userRepository;

        public TodoController(ITodoRepository todoRepository, IUserRepository userRepository)
        {
            _todoRepository = todoRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserTodos()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var todos = await _todoRepository.GetByUserIdAsync(userId);
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodo(int id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] TodoListDto todoDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var todo = new TodoList
            {
                Description = todoDto.Description,
                UserId = userId
            };

            var createdTodo = await _todoRepository.CreateAsync(todo);
            return CreatedAtAction(nameof(GetTodo), new { id = createdTodo.Id }, createdTodo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] TodoListDto todoDto)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            if (todo == null) return NotFound();

            todo.Description = todoDto.Description;
            await _todoRepository.UpdateAsync(todo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var result = await _todoRepository.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPost("{todoId}/tasks")]
        public async Task<IActionResult> AddTask(int todoId, [FromBody] TodoTaskDto taskDto)
        {
            var task = new TodoTask
            {
                Description = taskDto.Description,
                TodoId = todoId,
                DueDate = taskDto.DueDate,
                IsCompleted = false
            };

            var createdTask = await _todoRepository.AddTaskAsync(task);
            return CreatedAtAction(nameof(GetTodo), new { id = todoId }, createdTask);
        }

        [HttpPut("{todoId}/tasks/{taskId}")]
        public async Task<IActionResult> UpdateTask(int todoId, int taskId, [FromBody] TodoTaskDto taskDto)
        {
            var task = new TodoTask
            {
                Id = taskId,
                Description = taskDto.Description,
                TodoId = todoId,
                DueDate = taskDto.DueDate,
                IsCompleted = taskDto.IsCompleted
            };

            await _todoRepository.UpdateTaskAsync(task);
            return NoContent();
        }

        [HttpDelete("{todoId}/tasks/{taskId}")]
        public async Task<IActionResult> DeleteTask(int todoId, int taskId)
        {
            var result = await _todoRepository.DeleteTaskAsync(taskId);
            if (!result) return NotFound();
            return NoContent();
        }
    }

    public class TodoListDto
    {
        public string Description { get; set; } = string.Empty;
    }

    public class TodoTaskDto
    {
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
