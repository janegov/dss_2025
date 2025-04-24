using Microsoft.EntityFrameworkCore;
using Todo.Domain.Models;

namespace Todo.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TodoList> Todos { get; set; }
        public DbSet<TodoTask> Tasks { get; set; }
    }
}
