using AutoMapper;
using System;

namespace ToDoApp.WebAPI.ApiModels
{
    public class ToDoItemView
    {
        public Guid Id { get; set; }
        public String ItemContent { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Completed { get; set; }
        public DateTime? CompletedAt { get; set; }
    }

    public class ToDoItemInput
    {
        public Guid Id { get; set; }
        public String ItemContent { get; set; }
    }

    public class ModelsMapperProfile : Profile
    {
        public ModelsMapperProfile() : base()
        {
            CreateMap<CoreObjects.Entities.ToDoItem, ToDoItemView>();
            CreateMap<ToDoItemInput, CoreObjects.Entities.ToDoItem>();
        }
    }
}
