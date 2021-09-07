using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Core.Common;
using TodoList.Infrastructure.EntityFramework.Contexts;
using TodoList.Infrastructure.EntityFramework.Interfaces;

namespace TodoList.Infrastructure.EntityFramework.Repositories
{
    internal class ToDoListRepository : IToDoListRepository
    {
        private readonly TodoListDbContext _context;

        public ToDoListRepository(TodoListDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDoList>> GetAllAsync()
        {
            return await _context.Lists.ToArrayAsync();   
        }

        public Task<ToDoList> GetByIDAsync(string id)
        {
            return _context.Lists.SingleOrDefaultAsync(_ => _.ID == id);
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItems(string listId)
        {
            var items = await _context.Items.Where(_ => _.ListId == listId).ToArrayAsync();
            return items;
        }

        public async Task<ToDoList> InsertAsync(ToDoList resource)
        {
            _context.Add(resource);
            await _context.SaveChangesAsync();
            return resource;
        }

        public async Task<ToDoList> UpdateAsync(ToDoList resource)
        {
            _context.Update(resource);
            await _context.SaveChangesAsync();
            return resource;
        }

        public async Task DeleteAsync(string id)
        {
            var list = await GetByIDAsync(id);

            if (list is null)
            {
                return;
            }

            _context.Remove(list);
            await _context.SaveChangesAsync();
        }
    }
}
