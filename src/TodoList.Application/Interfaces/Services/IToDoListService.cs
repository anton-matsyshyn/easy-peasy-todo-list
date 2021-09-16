using Peasy;
using System.Collections.Generic;
using TodoList.Core.Common;

namespace TodoList.Application.Interfaces.Services
{
    public interface IToDoListService : IService<ToDoList, string>
    {
        ICommand<IEnumerable<TodoItem>> GetTodoItemsCommand(string listId);
    }
}
