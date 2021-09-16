using Peasy;
using System.Threading.Tasks;

namespace TodoList.Application.Tests.Mocks
{
    public class AlwaysValidRule : RuleBase
    {
        protected override Task OnValidateAsync() => Task.CompletedTask;
    }
}
