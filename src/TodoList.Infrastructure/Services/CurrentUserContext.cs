using System.Threading.Tasks;
using TodoList.Application.Interfaces;
using TodoList.Core.Common;
using TodoList.Infrastructure.Mocks;

namespace TodoList.Infrastructure.Services
{
    public class CurrentUserContext : ICurrentUserContext
    {
        public Task<User> GetCurrentUser()
        {
            var entity = UserMock.Users[0];
            var user = new User
            {
                ID = entity.ID,
                BirthDate = entity.BirthDate,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
            };
            return Task.FromResult(user);
        }
    }
}
