﻿using System;
using ToDoApp.CoreObjects.Entities;

namespace ToDoApp.CoreObjects.RepoInterfaces
{
    public interface IToDoItemsRepository : IGenericRepository<ToDoItem, Guid>
    {

    }
}