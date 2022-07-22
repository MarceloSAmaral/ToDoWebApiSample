using System;
using ToDoApp.CoreObjects.Entities;
using ToDoApp.CoreObjects.RepoInterfaces;

namespace ToDoApp.Data
{
    public class UsersRepository : GenericRepository<User, Guid>, IUsersRepository
    {
        public UsersRepository(ToDoAppContext context) : base(context)
        {
        }
    }
}
