using System;
using ToDoApp.CoreObjects.Entities;
using ToDoApp.CoreObjects.RepoInterfaces;

namespace ToDoApp.Data
{
    public class ToDoItemsRepository : GenericRepository<ToDoItem, Guid>, IToDoItemsRepository
    {
        public ToDoItemsRepository(ToDoAppContext context) : base(context)
        {
        }
    }
}
