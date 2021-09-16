using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using TodoList.Application.Interfaces.DataProxies;
using TodoList.Application.Rules;
using TodoList.Core.Common;

namespace TodoList.Application.Tests.Rules
{
    [TestClass]
    public class UserExistsRuleTests
    {
        private UserExistsRule _testee;

        private string _userId;

        private Mock<IUserDataProxy> _userDataProxy;

        [TestInitialize]
        public async Task Initialize()
        {
            _userId = Guid.NewGuid().ToString();
            _userDataProxy = new Mock<IUserDataProxy>();
        }

        [TestMethod]
        public async Task is_valid_if_proxy_returned_not_null()
        {
            _userDataProxy
               .Setup(
                   p => p.GetByIDAsync(_userId)
               )
               .ReturnsAsync(new User());
            _testee = new UserExistsRule(_userId, _userDataProxy.Object);            

            var result = await _testee.ExecuteAsync();

            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public async Task is_invalid_if_proxy_returned_null()
        {
            var errorMessage = $"User with id {_userId} does not exist";

            _userDataProxy
               .Setup(
                   p => p.GetByIDAsync(_userId)
               )
               .Returns(Task.FromResult<User>(null));
            _testee = new UserExistsRule(_userId, _userDataProxy.Object);

            var result = await _testee.ExecuteAsync();

            result.IsValid.Should().BeFalse();
            result.ErrorMessage.Should().BeEquivalentTo(errorMessage);
        }
    }
}
