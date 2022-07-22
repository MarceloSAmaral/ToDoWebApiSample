using System;
using System.Threading.Tasks;

namespace ToDoApp.CoreObjects.RepoInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();

        IToDoItemsRepository ToDoItemsRepository { get; }

        IUsersRepository UsersRepository { get; }
    }
}
