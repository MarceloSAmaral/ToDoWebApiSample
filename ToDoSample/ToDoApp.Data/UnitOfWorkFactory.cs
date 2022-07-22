using System;
using ToDoApp.CoreObjects.RepoInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoApp.Data
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public UnitOfWorkFactory(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }
        public IUnitOfWork Create()
        {
            var context = ServiceProvider.GetService<ToDoAppContext>();
            return new UnitOfWork(context);
        }
    }
}
