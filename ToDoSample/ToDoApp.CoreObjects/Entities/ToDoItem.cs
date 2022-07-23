using System;

namespace ToDoApp.CoreObjects.Entities
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public String ItemContent { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public bool Completed { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }

    }
}
