using Peasy;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Application.Interfaces;
using TodoList.Application.Interfaces.DataProxies;
using TodoList.Application.Interfaces.Factories;
using TodoList.Core.Common;
using TodoList.Core.Enums;

namespace TodoList.Application.Commands
{
    public class UpdateToDoListCommand : CommandBase<ToDoList>
    {
        private readonly IToDoListDataProxy _dataProxy;
        private readonly INotificationSender _notificationSender;        
        private readonly ICurrentUserContext _currentUserContext;
        private readonly IRuleFactory _ruleFactory;

        public UpdateToDoListCommand(
                ToDoList list,
                IToDoListDataProxy dataProxy,
                INotificationSender notificationSender,
                ICurrentUserContext currentUserContext,
                IRuleFactory ruleFactory
            )
        {
            List = list;
            _dataProxy = dataProxy;
            _notificationSender = notificationSender;
            _currentUserContext = currentUserContext;
            _ruleFactory = ruleFactory;
        }

        private ToDoList List { get; }
        private IEnumerable<TodoItem> WorkTodoItems { get; set; }

        protected override async Task OnInitializationAsync()
        {
            var todoListItems = await _dataProxy.GetTodoItems(List.ID);
            WorkTodoItems = todoListItems.Where(_ => _.Type == TaskType.Work);
        }

        protected override async Task<IEnumerable<IRule>> OnGetRulesAsync()
        {
            var currentUser = await _currentUserContext.GetCurrentUser();

            return new[]
            {
                _ruleFactory.UserIsAssociatedWithList(currentUser.ID, List.ID)
            };
        }

        protected override async Task<ToDoList> OnExecuteAsync()
        {
            throw new System.Exception();
            if (List.FinishDate is null)
            {
                var notificationTasks = WorkTodoItems.Select(
                                            _ => _notificationSender.Notify(
                                                    new Message(_.Title, _.Description)
                                                )
                                        );

                await Task.WhenAll(notificationTasks);
            }

            return await _dataProxy.UpdateAsync(List);
        }
    }
}
