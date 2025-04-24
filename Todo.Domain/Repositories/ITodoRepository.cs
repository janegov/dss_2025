using Todo.Domain.Models;

namespace Todo.Domain.Repositories
{
    public interface ITodoRepository
    {
        Task<TodoList?> GetByIdAsync(int id);
        Task<List<TodoList>> GetByUserIdAsync(int userId);
        Task<TodoList> CreateAsync(TodoList todoList);
        Task<TodoList> UpdateAsync(TodoList todoList);
        Task<bool> DeleteAsync(int id);
        Task<TodoTask> AddTaskAsync(TodoTask task);
        Task<TodoTask> UpdateTaskAsync(TodoTask task);
        Task<bool> DeleteTaskAsync(int taskId);
    }
}
