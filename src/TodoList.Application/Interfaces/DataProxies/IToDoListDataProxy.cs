using Peasy;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Core.Common;

namespace TodoList.Application.Interfaces.DataProxies
{
    public interface IToDoListDataProxy : IDataProxy<ToDoList, string>
    {
        Task<IEnumerable<TodoItem>> GetTodoItems(string listId);
        Task<bool> CheckIfUserAssociatedWithList(string userId, string listId);
    }
}
