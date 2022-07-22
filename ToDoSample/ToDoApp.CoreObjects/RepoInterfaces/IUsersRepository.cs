using System;
using ToDoApp.CoreObjects.Entities;

namespace ToDoApp.CoreObjects.RepoInterfaces
{
    public interface IUsersRepository : IGenericRepository<User,Guid>
    {

    }
}
