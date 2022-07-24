using System;

namespace ToDoApp.WebAPI.ApiModels
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        public String ItemContent { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public bool Completed { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
    }
}
