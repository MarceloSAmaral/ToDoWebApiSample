using System;
using System.Collections.Generic;
using ToDoApp.CoreObjects.Entities;

namespace ToDoApp.CoreObjects.RepoInterfaces
{
    public interface IToDoItemsRepository : IGenericRepository<ToDoItem, Guid>
    {
        IEnumerable<ToDoItem> GetByUserId(Guid userId);
    }
}
