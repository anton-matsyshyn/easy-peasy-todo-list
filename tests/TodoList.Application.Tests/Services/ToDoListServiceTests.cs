using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Application.Interfaces;
using TodoList.Application.Interfaces.DataProxies;
using TodoList.Application.Interfaces.Factories;
using TodoList.Application.Services;
using TodoList.Application.Tests.Mocks;
using TodoList.Core.Common;
using TodoList.Core.Enums;

namespace TodoList.Application.Tests.Services
{
    [TestClass]
    public class ToDoListServiceTests
    {
        private ToDoListService _testee;

        private User _currentUser;
        private string _todoListId;
        private List<TodoItem> _todoItems;

        private Mock<IToDoListDataProxy> _mockTodoListDataProxy;
        private Mock<INotificationSender> _mockNotificationSender;
        private Mock<ICurrentUserContext> _mockCurrentUserContext;
        private Mock<IRuleFactory> _mockRuleFactory;

        [TestInitialize]
        public async Task Initialize()
        {
            _currentUser = new User
            {
                ID = Guid.NewGuid().ToString(),
                FirstName = "Tony",
                LastName = "Stark",
                Email = "tony.stark@email.com"
            };
            _todoListId = Guid.NewGuid().ToString();
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

            _mockTodoListDataProxy = new Mock<IToDoListDataProxy>();
            _mockNotificationSender = new Mock<INotificationSender>();
            _mockCurrentUserContext = new Mock<ICurrentUserContext>();
            _mockRuleFactory = new Mock<IRuleFactory>();

            _mockTodoListDataProxy
                .Setup(
                    p => p.GetTodoItems(It.IsAny<string>())
                )
                .ReturnsAsync(_todoItems);
            _mockCurrentUserContext
                .Setup(
                    p => p.GetCurrentUser()
                )
                .ReturnsAsync(_currentUser);
            _mockRuleFactory
                .Setup(
                    p => p.UserIsAssociatedWithList(
                            _currentUser.ID,
                            _todoListId
                        )
                )
                .Returns(new AlwaysValidRule());

            _testee = new ToDoListService(
                    _mockTodoListDataProxy.Object,
                    _mockNotificationSender.Object,
                    _mockCurrentUserContext.Object,
                    _mockRuleFactory.Object
                );
        }

        [TestMethod]
        public async Task should_call_data_proxy()
        {
            var result = await _testee.GetTodoItemsCommand(_todoListId).ExecuteAsync();

            result.Success.Should().BeTrue();
            _mockTodoListDataProxy
                .Verify(
                    p => p.GetTodoItems(_todoListId)
                );
        }

        [TestMethod]
        public async Task should_call_get_current_user()
        {
            var result = await _testee.GetTodoItemsCommand(_todoListId).ExecuteAsync();

            result.Success.Should().BeTrue();
            _mockCurrentUserContext
                .Verify(
                    p => p.GetCurrentUser()
                );
        }

        [TestMethod]
        public async Task should_call_rule_factory()
        {
            var result = await _testee.GetTodoItemsCommand(_todoListId).ExecuteAsync();

            result.Success.Should().BeTrue();
            _mockRuleFactory
                .Verify(
                    p => p.UserIsAssociatedWithList(_currentUser.ID, _todoListId)
                );
        }
    }
}
