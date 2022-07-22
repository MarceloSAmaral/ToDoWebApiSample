using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ToDoApp.CoreObjects.AppInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using ToDoApp.CoreObjects.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private IServiceProvider _ServiceProvider { get; }
        private IToDoItemsApplication _ToDoItemsApplication { get; }

        public ToDoItemController(IServiceProvider serviceProvider)
        {
            _ServiceProvider = serviceProvider;
            _ToDoItemsApplication = serviceProvider.GetService<IToDoItemsApplication>();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetAsync()
        {
            var currentUser = GetCurrentUser();
            var toDoItems = await _ToDoItemsApplication.GetItemsAsync(currentUser.Id);
            foreach (var item in toDoItems)
            {
                //ConvertToView
            }
            return new OkObjectResult(toDoItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetAsync(Guid id)
        {
            var currentUser = GetCurrentUser();
            var toDoItem = await _ToDoItemsApplication.GetItemByIdAsync(id);
            if (toDoItem == null) return new NotFoundResult();
            if (currentUser.Id != toDoItem.UserId) return new UnauthorizedResult();
            return new OkObjectResult(toDoItem);
        }

        [HttpPost]
        public async Task<IAsyncResult> PostAsync([FromBody] string value)
        {
            var currentUser = GetCurrentUser();
            ValidateToDoItem(value);
            ValidateAuthor(currentUser, value);
            ToDoItem item = new ToDoItem();
            await _ToDoItemsApplication.AddItemAsync(item);
            
            return (IAsyncResult)CreatedAtAction(nameof(GetAsync), new { id = item.Id }, item);
        }



        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            var currentUser = GetCurrentUser();
            ValidateToDoItem(value);
            ValidateAuthor(currentUser, value);
            ToDoItem item = new ToDoItem();
            //await _ToDoItemsApplication.AddItemAsync(item);

            //return (IAsyncResult)CreatedAtAction(nameof(GetAsync), new { id = item.Id }, item);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private User GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        private void ValidateToDoItem(string value)
        {
            throw new NotImplementedException();
        }

        private void ValidateAuthor(User currentUser, string value)
        {
            throw new NotImplementedException();
        }
    }
}
