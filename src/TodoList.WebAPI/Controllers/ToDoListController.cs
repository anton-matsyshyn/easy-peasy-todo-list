using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoList.Application.Interfaces.Services;
using TodoList.Core.Common;

namespace TodoList.WebAPI.Controllers
{
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListService _service;

        public ToDoListController(IToDoListService service)
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

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetItemsByList(string id)
        {
            return Ok(await _service.GetTodoItemsCommand(id).ExecuteAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ToDoList payload)
        {
            var result = await _service.InsertCommand(payload).ExecuteAsync();
            return StatusCode(201, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ToDoList payload)
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
