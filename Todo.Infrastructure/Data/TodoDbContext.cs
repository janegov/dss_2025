using Microsoft.EntityFrameworkCore;
using Todo.Domain.Models;
using Todo.Infrastructure.Mappings;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Todo.Infrastructure.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<TodoList> TodoLists { get; set; } = null!;
        public DbSet<TodoTask> TodoTasks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;Database=TodoDb;User=root;Password=01012006Yanagovenko;",
                    ServerVersion.AutoDetect("Server=localhost;Database=TodoDb;User=root;Password=01012006Yanagovenko;"),
                    mySqlOptions => mySqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore)
                );
            }
        }
    }
}
