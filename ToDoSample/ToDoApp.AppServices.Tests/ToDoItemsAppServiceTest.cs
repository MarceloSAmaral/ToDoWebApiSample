using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Threading.Tasks;
using ToDoApp.Base;
using ToDoApp.CoreObjects.AppInterfaces;
using ToDoApp.CoreObjects.Entities;
using ToDoApp.CoreObjects.RepoInterfaces;
using Xunit;

namespace ToDoApp.AppServices.Tests
{
    public class ToDoItemsAppServiceTest
    {
        public Mock<IServiceProvider> _serviceProviderFake { get; set; }
        public Mock<IUnitOfWorkFactory> _unitOfWorkFactoryFake { get; set; }
        public Mock<IUnitOfWork> _unitOfWorkFake { get; set; }
        public Mock<IToDoItemsRepository> _todoItemsRepoFake { get; set; }
        public Mock<IUsersApplication> _usersAppServiceFake { get; set; }

        public ToDoItemsAppServiceTest()
        {
            _serviceProviderFake = new Mock<IServiceProvider>();
            _unitOfWorkFactoryFake = new Mock<IUnitOfWorkFactory>();
            _unitOfWorkFake = new Mock<IUnitOfWork>();
            _todoItemsRepoFake = new Mock<IToDoItemsRepository>();
            _usersAppServiceFake = new Mock<IUsersApplication>();
        }

        private void BasicMocksSetup()
        {
            _serviceProviderFake.Setup(x => x.GetService(typeof(IUsersApplication))).Returns(_usersAppServiceFake.Object);
            _unitOfWorkFactoryFake.Setup(x => x.Create()).Returns(_unitOfWorkFake.Object);
            _unitOfWorkFake.Setup(x => x.ToDoItemsRepository).Returns(_todoItemsRepoFake.Object);
        }

        [Fact]
        public async Task UserCannotUpdateACompletedToDoItem()
        {
            Guid currendUserId = Guid.NewGuid();
            User currentUser = new User()
            {
                Id = currendUserId,
            };
            Guid alreadyCompletedToDoItemId = Guid.NewGuid();
            CoreObjects.Entities.ToDoItem alreadyCompletedToDoItem = new ToDoItem()
            {
                Id = alreadyCompletedToDoItemId,
                UserId = currendUserId,
                ItemContent = Guid.NewGuid().ToString(),
                Completed = true,
                CompletedAt = TimeProvider.Current.UtcNow,
            };

            CoreObjects.Entities.ToDoItem userInputItem = new ToDoItem()
            {
                Id = alreadyCompletedToDoItemId,
                UserId = currendUserId,
                ItemContent = Guid.NewGuid().ToString(),
                Completed = true,
                CompletedAt = TimeProvider.Current.UtcNow,
            };

            BasicMocksSetup();
            _usersAppServiceFake.Setup(x => x.GetUserByIdAsync(currendUserId)).ReturnsAsync(currentUser);
            _todoItemsRepoFake.Setup(x => x.GetByKeyAsync(alreadyCompletedToDoItemId)).ReturnsAsync(alreadyCompletedToDoItem);
            ToDoItemsApplication itemsApplication = new ToDoItemsApplication(_serviceProviderFake.Object, _unitOfWorkFactoryFake.Object);
            await Assert.ThrowsAsync<CannotUpdateCompletedToDoItemException>(() => itemsApplication.UpdateToDoItemAsync(currentUser, userInputItem));
        }


        /*
         * Other tests that would be applicable to this project:

                Creating a ToDoItem with an existing ToDoItemId results in OK
                Creating a ToDoItem with status Completed results in exception
                Updating a ToDoItem alters its UpdatedAt timestamp
                Completing a ToDoItem alters its CompletedAt timestamp
                Completing an already completed ToDoItem results in OK
                Completing an already completed ToDoItem does not change its CompletedAt timestamp
                An user cannot create a ToDoItem for / in behalf of another user
                An user cannot read, modify or delete a ToDoItem belonging to another user

         */
    }
}
