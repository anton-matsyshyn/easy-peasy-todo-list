using Peasy;
using TodoList.Core.Common;

namespace TodoList.Application.Interfaces.Services
{
    public interface ITodoItemService : IService<TodoItem, string>
    { }
}
