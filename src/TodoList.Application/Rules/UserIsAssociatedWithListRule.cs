using Peasy;
using System.Threading.Tasks;
using TodoList.Application.Interfaces;
using TodoList.Application.Interfaces.DataProxies;

namespace TodoList.Application.Rules
{
    public class UserIsAssociatedWithListRule : RuleBase
    {
        private readonly IToDoListDataProxy _dataProxy;
        private readonly ICurrentUserContext _currentUserContext;

        public UserIsAssociatedWithListRule(
                string userId,
                string listId,
                IToDoListDataProxy dataProxy,
                ICurrentUserContext currentUserContext
            )
        {
            UserId = userId;
            ListId = listId;
            _dataProxy = dataProxy;
            _currentUserContext = currentUserContext;
        }

        private string UserId { get; }
        private string ListId { get; }

        protected override async Task OnValidateAsync()
        {
            var currentUser = await _currentUserContext.GetCurrentUser();

            if (currentUser is null)
            {
                Invalidate("User is not authorized");
            }

            var userHasAccessToList = await _dataProxy.CheckIfUserAssociatedWithList(
                    UserId,
                    ListId
                );

            if (!userHasAccessToList)
            {
                Invalidate($"User with id {currentUser.ID} cannot access list ${ListId}");
            }
        }
    }
}
