using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Application.Commands;
using TodoList.Application.Interfaces;
using TodoList.Application.Interfaces.DataProxies;
using TodoList.Application.Interfaces.Factories;
using TodoList.Application.Tests.Mocks;
using TodoList.Core.Common;
using TodoList.Core.Enums;

namespace TodoList.Application.Tests.Commands
{
    [TestClass]
    public class UpdateToDoListCommandTests
    {
        private UpdateToDoListCommand _testee;

        private ToDoList _toDoList;
        private List<TodoItem> _todoItems;
        private User _currentUser;

        private Mock<IToDoListDataProxy> _mockTodoListDataProxy;
        private Mock<INotificationSender> _mockNotificationSender;
        private Mock<ICurrentUserContext> _mockCurrentUserContext;
        private Mock<IRuleFactory> _mockRuleFactory; 

        [TestInitialize]
        public async Task Initialize()
        {
            _toDoList = new ToDoList
            {
                ID = Guid.NewGuid().ToString(),
                Description = "Todolist description",
                CreatorId = Guid.NewGuid().ToString(),
                StartDate = DateTime.Now.AddDays(3),             
            };
            _todoItems = new List<TodoItem>
            {
                new TodoItem
                {
                    ID = Guid.NewGuid().ToString(),
                    Type = TaskType.Work,
                    Title = "Title 1",
                    Description = "Description 1",
                },
                new TodoItem
                {
                    ID = Guid.NewGuid().ToString(),
                    Type = TaskType.Work,
                    Title = "Title 2",
                    Description = "Description 2",
                },
                new TodoItem
                {
                    ID = Guid.NewGuid().ToString(),
                    Type = TaskType.SelfDevelopment,
                    Title = "Title 3",
                    Description = "Description 3",
                },
            };
            _currentUser = new User
            {
                ID = _toDoList.CreatorId,
                FirstName = "Tony",
                LastName = "Stark",
                Email = "tony.stark@email.com"
            };

            _mockTodoListDataProxy = new Mock<IToDoListDataProxy>();
            _mockNotificationSender = new Mock<INotificationSender>();
            _mockCurrentUserContext = new Mock<ICurrentUserContext>();
            _mockRuleFactory = new Mock<IRuleFactory>();

            _mockTodoListDataProxy
                .Setup(
                    p => p.GetTodoItems(_toDoList.ID)
                )
                .ReturnsAsync(_todoItems);
            _mockTodoListDataProxy
                .Setup(
                    p => p.UpdateAsync(_toDoList)
                )
                .ReturnsAsync(_toDoList);

            _mockCurrentUserContext
                .Setup(
                    p => p.GetCurrentUser()
                )
                .ReturnsAsync(_currentUser);

            _mockRuleFactory
                .Setup(
                    p => p.UserIsAssociatedWithList(It.IsAny<string>(), It.IsAny<string>())
                )
                .Returns(new AlwaysValidRule());
        }
    
        [TestMethod]
        public async Task should_call_data_proxy()
        {
            _testee = new UpdateToDoListCommand(
                _toDoList,
                _mockTodoListDataProxy.Object,
                _mockNotificationSender.Object,
                _mockCurrentUserContext.Object,
                _mockRuleFactory.Object
            );

            var result = await _testee.ExecuteAsync();

            result.Success.Should().BeTrue();
            result.Value.Should().Be(_toDoList);
            _mockTodoListDataProxy
                .Verify(
                    p => p.UpdateAsync(_toDoList)
                );
        }

        [TestMethod]
        public async Task should_call_notifiyer_for_each_work_item()
        {
            _testee = new UpdateToDoListCommand(
                _toDoList,
                _mockTodoListDataProxy.Object,
                _mockNotificationSender.Object,
                _mockCurrentUserContext.Object,
                _mockRuleFactory.Object
            );

            var result = await _testee.ExecuteAsync();
            var workItems = _todoItems.Where(_ => _.Type == TaskType.Work);

            result.Success.Should().BeTrue();
            foreach (var item in workItems)
            {
                _mockNotificationSender
                    .Verify(
                        p => p.Notify(
                                It.Is<Message>(
                                        _ => _.Title == item.Title &&
                                        _.Text == item.Description
                                    )
                            )
                    );
            }
        }

        [TestMethod]
        public async Task should_not_call_notifyer_if_todolist_finished()
        {
            _toDoList = new ToDoList
            {
                ID = Guid.NewGuid().ToString(),
                Description = "Todolist description",
                CreatorId = Guid.NewGuid().ToString(),
                StartDate = DateTime.Now.AddDays(3),
                FinishDate = DateTime.Now.AddDays(14),
            };

            _testee = new UpdateToDoListCommand(
                    _toDoList,
                    _mockTodoListDataProxy.Object,
                    _mockNotificationSender.Object,
                    _mockCurrentUserContext.Object,
                    _mockRuleFactory.Object
                );

            var result = await _testee.ExecuteAsync();

            result.Success.Should().BeTrue();
            _mockNotificationSender
                .Verify(
                    p => p.Notify(It.IsAny<Message>()),
                    Times.Never()
                );
        }
    }
}
