namespace Todo.Domain.Models
{
    public class TodoTask
    {
        public int? Id { get; set; }
        public string? Description { get; set; }
        public int? TodoId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
        public TodoList? Holder { get; set; }
    }
}
