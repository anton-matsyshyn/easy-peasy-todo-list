using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoList.Application.Commands;
using TodoList.Application.Interfaces;
using TodoList.Application.Interfaces.DataProxies;

namespace TodoList.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IToDoListDataProxy _toDoListDataProxy;
        private readonly IJobScheduler _jobScheduler;

        public JobController(
                IToDoListDataProxy toDoListDataProxy,
                IJobScheduler jobScheduler
            )
        {
            _toDoListDataProxy = toDoListDataProxy;
            _jobScheduler = jobScheduler;
        }

        [HttpPost("{listId}")]
        public async Task<IActionResult> RunTodoListJob(string listId)
        {
            var command = new RunRemoteJobCommand(
                    listId,
                    _toDoListDataProxy,
                    _jobScheduler
                );

            return Ok(await command.ExecuteAsync());
        }
    }
}
