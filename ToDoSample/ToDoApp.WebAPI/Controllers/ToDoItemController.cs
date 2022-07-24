﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ToDoApp.CoreObjects.AppInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using ToDoApp.CoreObjects.Entities;
using ToDoApp.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ToDoApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoItemController : ControllerBase
    {
        private IServiceProvider _ServiceProvider { get; }
        private IToDoItemsApplication _ToDoItemsApplication { get; }

        public ToDoItemController(IServiceProvider serviceProvider)
        {
            _ServiceProvider = serviceProvider;
            _ToDoItemsApplication = serviceProvider.GetService<IToDoItemsApplication>();
        }

        [HttpGet("/List")]
        public async Task<IActionResult> List()
        {
            try
            {
                var currentUser = await GetCurrentUserAsync();
                var toDoItems = _ToDoItemsApplication.GetItems(currentUser);
                return new OkObjectResult(toDoItems);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpGet("/Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var currentUser = await GetCurrentUserAsync();
                var toDoItem = await _ToDoItemsApplication.GetItemByIdAsync(currentUser, id);
                if (toDoItem == null) return new NotFoundResult();
                return new OkObjectResult(toDoItem);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPost("/Add")]
        public async Task<IActionResult> Add([FromBody] ToDoItem value)
        {
            try
            {
                var currentUser = await GetCurrentUserAsync();
                await _ToDoItemsApplication.CreateToDoItemAsync(currentUser, value);
                return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPost("/Update")]
        public async Task<IActionResult> Update([FromBody] ToDoItem value)
        {
            try
            {
                var currentUser = await GetCurrentUserAsync();
                await _ToDoItemsApplication.UpdateToDoItemAsync(currentUser, value);
                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPut("/Complete/{id}")]
        public async Task<IActionResult> Complete(Guid id)
        {
            try
            {
                var currentUser = await GetCurrentUserAsync();
                await _ToDoItemsApplication.CompleteToDoItemAsync(currentUser, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpDelete("/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var currentUser = await GetCurrentUserAsync();
                await _ToDoItemsApplication.DeleteToDoItemAsync(currentUser, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [NonAction]
        public virtual async Task<User> GetCurrentUserAsync()
        {
            //Lets skip Authentication for a while and assume that this api is being used by the *DefaultUser*
            var usersAppService = _ServiceProvider.GetService<IUsersApplication>();
            var currentUser = await usersAppService.GetUserByIdAsync(new Guid("00000000-0000-0000-0001-000000000001"));
            return currentUser;
        }

        [NonAction]
        public virtual IActionResult Error(Exception exception)
        {
            if(exception is BaseException)
            {
                if(exception is NotAuthorizedException)
                {
                    return new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                }

                return new BadRequestObjectResult(exception.Message);
            }
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
