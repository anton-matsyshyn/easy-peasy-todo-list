using Peasy;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Core.Common;

namespace TodoList.Infrastructure.EntityFramework.Interfaces
{
    public interface IToDoListRepository : IDataProxy<ToDoList, string>
    {
        Task<IEnumerable<TodoItem>> GetTodoItems(string listId);
    }
}
