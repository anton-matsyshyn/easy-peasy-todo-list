using Peasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TodoList.Application.Interfaces;
using TodoList.Application.Interfaces.DataProxies;
using TodoList.Core.Common;
using TodoList.Core.Enums;

namespace TodoList.Application.Commands
{
    public class RunRemoteJobCommand : CommandBase
    {
        private readonly IToDoListDataProxy _toDoListDataProxy;
        private readonly IJobScheduler _jobScheduler;

        public RunRemoteJobCommand(
                string listId,
                IToDoListDataProxy toDoListDataProxy,
                IJobScheduler jobScheduler
            )
        {
            ListId = listId;
            _toDoListDataProxy = toDoListDataProxy;
            _jobScheduler = jobScheduler;
        }

        private string ListId { get; }
        private IEnumerable<TodoItem> ItemsWithNoType { get; set; }

        protected override async Task OnInitializationAsync()
        {
            var todoListItems = await _toDoListDataProxy.GetTodoItems(ListId);
            ItemsWithNoType = todoListItems.Where(_ => _.Type == TaskType.NotSet);
        }

        protected override Task OnExecuteAsync()
        {
            var scheduleJobTasks = ItemsWithNoType.Select(
                    _ => _jobScheduler.Schedule(new Job
                    {
                        Id = Guid.NewGuid().ToString(),
                        Prority = 8,
                        Data = JsonSerializer.Serialize(_)
                    })
                );

            return Task.WhenAll(scheduleJobTasks);
        }
    }
}
