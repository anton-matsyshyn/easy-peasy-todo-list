using Peasy;
using System.Collections.Generic;
using TodoList.Application.Commands;
using TodoList.Application.Interfaces;
using TodoList.Application.Interfaces.DataProxies;
using TodoList.Application.Interfaces.Factories;
using TodoList.Application.Interfaces.Services;
using TodoList.Core.Common;

namespace TodoList.Application.Services
{
    public class ToDoListService : ServiceBase<ToDoList, string>, IToDoListService
    {
        private readonly INotificationSender _notificationSender;
        private readonly ICurrentUserContext _currentUserContext;
        private readonly IRuleFactory _ruleFactory;

        public ToDoListService(
                IToDoListDataProxy dataProxy,
                INotificationSender notificationSender,
                ICurrentUserContext currentUserContext,
                IRuleFactory ruleFactory
            ) : base(dataProxy)
        {
            _notificationSender = notificationSender;
            _currentUserContext = currentUserContext;
            _ruleFactory = ruleFactory;
        }

        private IToDoListDataProxy GetDataProxy() => DataProxy as IToDoListDataProxy;

        public override ICommand<ToDoList> UpdateCommand(ToDoList resource)
        {
            return new UpdateToDoListCommand(
                    resource,
                    GetDataProxy(),
                    _notificationSender,
                    _currentUserContext,
                    _ruleFactory
                );
        }

        public ICommand<IEnumerable<TodoItem>> GetTodoItemsCommand(string listId)
        {
            string currentUserId = "";

            return new ServiceCommand<IEnumerable<TodoItem>>(
                    async () =>
                    {
                        var currentUser = await _currentUserContext.GetCurrentUser();
                        currentUserId = currentUser.ID;
                    },
                    async () => new[]
                    {
                        _ruleFactory.UserIsAssociatedWithList(currentUserId, listId)
                    },
                    () => GetDataProxy().GetTodoItems(listId)
                );
        }
    }
}
