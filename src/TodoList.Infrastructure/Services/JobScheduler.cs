using System.Threading.Tasks;
using TodoList.Application.Interfaces;
using TodoList.Core.Common;

namespace TodoList.Infrastructure.Services
{
    public class JobScheduler : IJobScheduler
    {
        public Task Schedule(Job job)
        {
            return Task.CompletedTask;
        }
    }
}
