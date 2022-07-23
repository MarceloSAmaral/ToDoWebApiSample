using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.CoreObjects.Entities;

namespace ToDoApp.CoreObjects.AppInterfaces
{
    public interface IToDoItemsApplication
    {
        Task CompleteToDoItemAsync(User currentUser, Guid toDoItemId);
        Task CreateToDoItemAsync(User currentUser, ToDoItem toDoItem);
        Task DeleteToDoItemAsync(User currentUser, Guid toDoItemId);
        Task UpdateToDoItemAsync(User currentUser, ToDoItem toDoItem);
        Task<ToDoItem> GetItemByIdAsync(Guid toDoItemId);
        IEnumerable<ToDoItem> GetItems(User currentUser);
    }
}
