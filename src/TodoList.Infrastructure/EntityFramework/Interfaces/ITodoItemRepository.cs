using Peasy;
using TodoList.Core.Common;

namespace TodoList.Infrastructure.EntityFramework.Interfaces
{
    public interface ITodoItemRepository : IDataProxy<TodoItem, string>
    { }
}
