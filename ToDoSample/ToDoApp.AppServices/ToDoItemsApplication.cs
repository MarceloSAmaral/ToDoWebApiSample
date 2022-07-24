using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Base;
using ToDoApp.CoreObjects.AppInterfaces;
using ToDoApp.CoreObjects.Entities;
using ToDoApp.CoreObjects.RepoInterfaces;


namespace ToDoApp.AppServices
{
    public class ToDoItemsApplication : IToDoItemsApplication
    {
        protected IServiceProvider ServiceProvider { get; }
        protected IUnitOfWorkFactory UowFactory { get; private set; }

        public ToDoItemsApplication(IServiceProvider serviceProvider, IUnitOfWorkFactory workFactory)
        {
            ServiceProvider = serviceProvider;
            UowFactory = workFactory;
        }

        public async Task CreateToDoItemAsync(User currentUser, ToDoItem toDoItem)
        {
            if (currentUser.Id != toDoItem.UserId) throw new NotAuthorizedException();

            using (var uow = UowFactory.Create())
            {
                var alreadyExistingItem = await uow.ToDoItemsRepository.GetByKeyAsync(toDoItem.Id);
                if (alreadyExistingItem != null) return;

                IUsersApplication usersApplication = ServiceProvider.GetService<IUsersApplication>();
                var user = await usersApplication.GetUserByIdAsync(toDoItem.UserId);
                if (user == null) throw new UserNotFoundException();

                if (toDoItem.Completed) throw new ToDoItemCannotBeCreatedAlreadyCompletedException();

                toDoItem.CreatedAt = TimeProvider.Current.UtcNow;
                toDoItem.UpdatedAt = toDoItem.CreatedAt;

                uow.ToDoItemsRepository.Insert(toDoItem);

                await uow.CommitAsync();
            }
        }

        public async Task UpdateToDoItemAsync(User currentUser, ToDoItem toDoItem)
        {
            if (currentUser.Id != toDoItem.UserId) throw new NotAuthorizedException();

            using (var uow = UowFactory.Create())
            {
                var alreadyExistingItem = await uow.ToDoItemsRepository.GetByKeyAsync(toDoItem.Id);
                if (alreadyExistingItem == null) throw new ToDoItemNotFoundException();

                IUsersApplication usersApplication = ServiceProvider.GetService<IUsersApplication>();
                var user = await usersApplication.GetUserByIdAsync(toDoItem.UserId);
                if (user == null) throw new UserNotFoundException();

                if (alreadyExistingItem.Completed) throw new CannotUpdateCompletedToDoItemException();

                alreadyExistingItem.UpdatedAt = TimeProvider.Current.UtcNow;
                alreadyExistingItem.ItemContent = toDoItem.ItemContent;

                uow.ToDoItemsRepository.Update(alreadyExistingItem);

                await uow.CommitAsync();
            }
        }

        public async Task CompleteToDoItemAsync(User currentUser, Guid toDoItemId)
        {
            using (var uow = UowFactory.Create())
            {
                var alreadyExistingItem = await uow.ToDoItemsRepository.GetByKeyAsync(toDoItemId);
                if (alreadyExistingItem == null) throw new ToDoItemNotFoundException();

                if (currentUser.Id != alreadyExistingItem.UserId) throw new NotAuthorizedException();

                if (alreadyExistingItem.Completed) return;

                alreadyExistingItem.CompletedAt = TimeProvider.Current.UtcNow;
                alreadyExistingItem.Completed = true;

                uow.ToDoItemsRepository.Update(alreadyExistingItem);

                await uow.CommitAsync();
            }
        }

        public async Task DeleteToDoItemAsync(User currentUser, Guid toDoItemId)
        {
            using (var uow = UowFactory.Create())
            {
                var alreadyExistingItem = await uow.ToDoItemsRepository.GetByKeyAsync(toDoItemId);
                if (alreadyExistingItem == null) return;

                if (currentUser.Id != alreadyExistingItem.UserId) throw new NotAuthorizedException();

                uow.ToDoItemsRepository.Delete(toDoItemId);

                await uow.CommitAsync();
            }
        }

        public async Task<ToDoItem> GetItemByIdAsync(User currentUser, Guid toDoItemId)
        {
            using (var uow = UowFactory.Create())
            {
                var todoItem = await uow.ToDoItemsRepository.GetByKeyAsync(toDoItemId);
                if (currentUser.Id != todoItem.UserId) throw new NotAuthorizedException();
                return todoItem;
            }
        }

        public IEnumerable<ToDoItem> GetItems(User currentUser)
        {
            using (var uow = UowFactory.Create())
            {
                return uow.ToDoItemsRepository.GetByUserId(currentUser.Id);
            }
        }
    }


}
