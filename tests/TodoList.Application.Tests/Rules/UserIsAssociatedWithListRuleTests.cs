using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using TodoList.Application.Interfaces;
using TodoList.Application.Interfaces.DataProxies;
using TodoList.Application.Rules;
using TodoList.Core.Common;

namespace TodoList.Application.Tests.Rules
{
    [TestClass]
    public class UserIsAssociatedWithListRuleTests
    {
        private UserIsAssociatedWithListRule _testee;

        private ToDoList _toDoList;
        private User _currentUser;

        private Mock<IToDoListDataProxy> _mockTodoListDataProxy;
        private Mock<ICurrentUserContext> _mockCurrentUserContext;

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
            _currentUser = new User
            {
                ID = _toDoList.CreatorId,
                FirstName = "Tony",
                LastName = "Stark",
                Email = "tony.stark@email.com"
            };

            _mockTodoListDataProxy = new Mock<IToDoListDataProxy>();
            _mockCurrentUserContext = new Mock<ICurrentUserContext>();
        }

        [TestMethod]
        public async Task is_invalid_when_current_user_null()
        {
            var errorMessage = "User is not authorized";

            _mockCurrentUserContext
                .Setup(
                    p => p.GetCurrentUser()
                )
                .Returns(Task.FromResult<User>(null));

            _mockTodoListDataProxy
                .Setup(
                    p => p.CheckIfUserAssociatedWithList(
                            It.IsAny<string>(),
                            It.IsAny<string>()
                        )
                )
                .ReturnsAsync(true);

            _testee = new UserIsAssociatedWithListRule(
                    _currentUser.ID,
                    _toDoList.ID,
                    _mockTodoListDataProxy.Object,
                    _mockCurrentUserContext.Object
                );

            var result = await _testee.ExecuteAsync();

            result.IsValid.Should().BeFalse();
            result.ErrorMessage.Should().BeEquivalentTo(errorMessage);
        }

        [TestMethod]
        public async Task is_invalid_when_user_isnt_associated_with_list()
        {
            var errorMessage = $"User with id {_currentUser.ID} cannot access list ${_toDoList.ID}";

            _mockCurrentUserContext
                .Setup(
                    p => p.GetCurrentUser()
                )
                .ReturnsAsync(_currentUser);

            _mockTodoListDataProxy
                .Setup(
                    p => p.CheckIfUserAssociatedWithList(
                            It.IsAny<string>(),
                            It.IsAny<string>()
                        )
                )
                .ReturnsAsync(false);

            _testee = new UserIsAssociatedWithListRule(
                    _currentUser.ID,
                    _toDoList.ID,
                    _mockTodoListDataProxy.Object,
                    _mockCurrentUserContext.Object
                );

            var result = await _testee.ExecuteAsync();

            result.IsValid.Should().BeFalse();
            result.ErrorMessage.Should().BeEquivalentTo(errorMessage);
        }

        [TestMethod]
        public async Task is_valid_when_user_is_authorized_and_associated_with_list()
        {
            _mockCurrentUserContext
                .Setup(
                    p => p.GetCurrentUser()
                )
                .ReturnsAsync(_currentUser);

            _mockTodoListDataProxy
                .Setup(
                    p => p.CheckIfUserAssociatedWithList(
                            It.Is<string>(_ => _ == _currentUser.ID),
                            It.Is<string>(_ => _ == _toDoList.ID)
                        )
                )
                .ReturnsAsync(true);

            _testee = new UserIsAssociatedWithListRule(
                    _currentUser.ID,
                    _toDoList.ID,
                    _mockTodoListDataProxy.Object,
                    _mockCurrentUserContext.Object
                );

            var result = await _testee.ExecuteAsync();

            result.IsValid.Should().BeTrue();
        }

    }
}
