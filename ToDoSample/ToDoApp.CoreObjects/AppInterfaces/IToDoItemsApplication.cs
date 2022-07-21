using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.CoreObjects.Entities;

namespace ToDoApp.CoreObjects.AppInterfaces
{
    public interface IToDoItemsApplication
    {
        Task AddItemAsync(ToDoItem item);
        Task UpdateItemAsync(ToDoItem item);
        Task CompleteItemAsync(Guid itemId);
        Task DeleteItemAsync(Guid itemId);
        Task<ToDoItem> GetItemByIdAsync(Guid itemId);
        Task<IEnumerable<ToDoItem>> GetItemsAsync(Guid userId);
    }
}
