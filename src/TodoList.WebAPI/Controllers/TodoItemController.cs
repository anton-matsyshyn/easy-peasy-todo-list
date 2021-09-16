using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoList.Application.Interfaces.Services;
using TodoList.Core.Common;

namespace TodoList.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemService _service;

        public TodoItemController(ITodoItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllCommand().ExecuteAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _service.GetByIDCommand(id).ExecuteAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoItem payload)
        {
            var result = await _service.InsertCommand(payload).ExecuteAsync();
            return StatusCode(201, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TodoItem payload)
        {
            return Ok(await _service.UpdateCommand(payload).ExecuteAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteCommand(id).ExecuteAsync();
            return NoContent();
        }
    }
}
