using System;
using System.Threading.Tasks;
using ToDoApp.Base;
using ToDoApp.CoreObjects.AppInterfaces;
using ToDoApp.CoreObjects.Entities;
using ToDoApp.CoreObjects.RepoInterfaces;


namespace ToDoApp.AppServices
{
    public class UserAppService : IUsersApplication
    {
        protected IServiceProvider ServiceProvider { get; }
        protected IUnitOfWorkFactory UowFactory { get; private set; }

        public UserAppService(IServiceProvider serviceProvider, IUnitOfWorkFactory workFactory)
        {
            ServiceProvider = serviceProvider;
            UowFactory = workFactory;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            using (var uow = UowFactory.Create())
            {
                var userData = await uow.UsersRepository.GetByKeyAsync(id);
                if (userData == null) throw new UserNotFoundException();
                return userData;
            }
        }
    }


}
