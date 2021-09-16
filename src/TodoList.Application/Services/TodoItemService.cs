using Peasy;
using TodoList.Application.Interfaces.DataProxies;
using TodoList.Application.Interfaces.Services;
using TodoList.Core.Common;

namespace TodoList.Application.Services
{
    public class TodoItemService : ServiceBase<TodoItem, string>, ITodoItemService
    {
        public TodoItemService(ITodoItemDataProxy dataProxy): base(dataProxy)
        { }

        private ITodoItemDataProxy GetDataProxy() => DataProxy as ITodoItemDataProxy;
    }
}
