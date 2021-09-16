using Peasy;

namespace TodoList.Application.Interfaces.Factories
{
    public interface IRuleFactory
    {
        IRule UserIsAssociatedWithList(string userId, string listId);
        IRule UserExists(string userId);
    }
}
