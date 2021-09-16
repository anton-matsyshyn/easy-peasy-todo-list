using System.Threading.Tasks;
using TodoList.Core.Common;

namespace TodoList.Application.Interfaces
{
    public interface IJobScheduler
    {
        Task Schedule(Job job);
    }
}
