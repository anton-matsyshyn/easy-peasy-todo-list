using Peasy;
using System.Threading.Tasks;
using TodoList.Application.Interfaces.DataProxies;

namespace TodoList.Application.Rules
{
    public class UserExistsRule : RuleBase
    {
        private readonly IUserDataProxy _userDataProxy;

        public UserExistsRule(
                string userId,
                IUserDataProxy userDataProxy
            )
        {
            UserId = userId;
            _userDataProxy = userDataProxy;
        }

        private string UserId { get; }

        protected override async Task OnValidateAsync()
        {
            var user = await _userDataProxy.GetByIDAsync(UserId);
            
            if (user is null)
            {
                Invalidate($"User with id {UserId} does not exist");
            }
        }
    }
}
