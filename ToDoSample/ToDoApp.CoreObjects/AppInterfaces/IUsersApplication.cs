using System;
using System.Threading.Tasks;
using ToDoApp.CoreObjects.Entities;

namespace ToDoApp.CoreObjects.AppInterfaces
{
    public interface IUsersApplication
    {
        Task<User> GetUserByIdAsync(Guid id);
    }
}
