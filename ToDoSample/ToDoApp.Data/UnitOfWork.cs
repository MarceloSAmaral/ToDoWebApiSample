using System;
using System.Threading.Tasks;
using ToDoApp.CoreObjects.RepoInterfaces;

namespace ToDoApp.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public ToDoAppContext _context;
        private UsersRepository _usersRepository;
        private ToDoItemsRepository _postsRepository;

        public UnitOfWork(ToDoAppContext context)
        {
            _context = context;
        }

        public UnitOfWork()
        {
            _context = new ToDoAppContext();
        }

        public IToDoItemsRepository ToDoItemsRepository
        {
            get
            {
                return _postsRepository = _postsRepository ?? new ToDoItemsRepository(_context);
            }
        }

        public IUsersRepository UsersRepository
        {
            get
            {
                return _usersRepository = _usersRepository ?? new UsersRepository(_context);
            }
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
