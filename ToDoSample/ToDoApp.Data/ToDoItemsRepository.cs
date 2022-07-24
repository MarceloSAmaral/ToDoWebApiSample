using System;
using System.Linq;
using System.Collections.Generic;
using ToDoApp.CoreObjects.Entities;
using ToDoApp.CoreObjects.RepoInterfaces;

namespace ToDoApp.Data
{
    public class ToDoItemsRepository : GenericRepository<ToDoItem, Guid>, IToDoItemsRepository
    {
        public ToDoItemsRepository(ToDoAppContext context) : base(context)
        {
        }

        public virtual IEnumerable<ToDoItem> GetByUserId(Guid userId)
        {
            return dbSet.Where(x => x.UserId == userId).ToList();
        }
    }
}
