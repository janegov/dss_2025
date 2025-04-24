namespace Todo.Domain.Models
{
    public class TodoList
    {
        public int? Id { get; set; }
        public string? Description { get; set; }
        public int? UserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfTasks { get; set; }
        public User? Owner { get; set; }
        public List<TodoTask> Tasks { get; set; } = new();
    }
}
