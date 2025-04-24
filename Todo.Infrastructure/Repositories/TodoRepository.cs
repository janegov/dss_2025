using Microsoft.EntityFrameworkCore;
using Todo.Domain.Models;
using Todo.Domain.Repositories;
using Todo.Infrastructure.Data;

namespace Todo.Infrastructure.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<TodoList?> GetByIdAsync(int id)
        {
            return await _context.TodoLists
                .Include(t => t.Tasks)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TodoList>> GetByUserIdAsync(int userId)
        {
            return await _context.TodoLists
                .Include(t => t.Tasks)
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<TodoList> CreateAsync(TodoList todoList)
        {
            todoList.Date = DateTime.UtcNow;
            todoList.IsActive = true;
            todoList.NumberOfTasks = 0;

            _context.TodoLists.Add(todoList);
            await _context.SaveChangesAsync();
            return todoList;
        }

        public async Task<TodoList> UpdateAsync(TodoList todoList)
        {
            _context.TodoLists.Update(todoList);
            await _context.SaveChangesAsync();
            return todoList;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var todoList = await GetByIdAsync(id);
            if (todoList == null) return false;

            _context.TodoLists.Remove(todoList);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TodoTask> AddTaskAsync(TodoTask task)
        {
            var todoList = await GetByIdAsync(task.TodoId ?? 0);
            if (todoList == null)
                throw new ArgumentException("Todo list not found");

            _context.TodoTasks.Add(task);
            todoList.NumberOfTasks++;
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TodoTask> UpdateTaskAsync(TodoTask task)
        {
            _context.TodoTasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            var task = await _context.TodoTasks.FindAsync(taskId);
            if (task == null) return false;

            var todoList = await GetByIdAsync(task.TodoId ?? 0);
            if (todoList != null)
            {
                todoList.NumberOfTasks--;
            }

            _context.TodoTasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
