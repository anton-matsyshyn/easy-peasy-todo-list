using Peasy;
using TodoList.Core.Common;

namespace TodoList.Application.Interfaces.DataProxies
{
    public interface ITodoItemDataProxy : IDataProxy<TodoItem, string>
    { }
}
