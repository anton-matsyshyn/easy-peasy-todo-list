using Peasy;
using System;
using TodoList.Application.Interfaces;
using TodoList.Application.Interfaces.DataProxies;
using TodoList.Application.Interfaces.Factories;
using TodoList.Application.Rules;

namespace TodoList.Application.Factories
{
    public class RuleFactory : IRuleFactory
    {
        private readonly IToDoListDataProxy _todoListDataProxy;
        private readonly IUserDataProxy _userDataProxy;
        private readonly ICurrentUserContext _currentUserContext;        

        public RuleFactory(
                IToDoListDataProxy todoListDataProxy,
                IUserDataProxy userDataProxy,
                ICurrentUserContext currentUserContext
            )
        {
            _todoListDataProxy = todoListDataProxy;
            _userDataProxy = userDataProxy;
            _currentUserContext = currentUserContext;
        }


        public IRule UserIsAssociatedWithList(string userId, string listId)
        {
            return new UserIsAssociatedWithListRule(
                    userId,
                    listId,
                    _todoListDataProxy,
                    _currentUserContext
                );
        }

        public IRule UserExists(string userId)
        {
            return new UserExistsRule(
                    userId,
                    _userDataProxy
                );
        }
    }
}
