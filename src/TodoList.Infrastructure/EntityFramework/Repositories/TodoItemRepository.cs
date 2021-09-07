using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Core.Common;
using TodoList.Infrastructure.EntityFramework.Contexts;
using TodoList.Infrastructure.EntityFramework.Interfaces;

namespace TodoList.Infrastructure.EntityFramework.Repositories
{
    internal class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoListDbContext _context;

        public TodoItemRepository(TodoListDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.Items.ToArrayAsync();
        }

        public Task<TodoItem> GetByIDAsync(string id)
        {
            return _context.Items.SingleOrDefaultAsync(_ => _.ID == id);
        }

        public async Task<TodoItem> InsertAsync(TodoItem resource)
        {
            _context.Add(resource);
            await _context.SaveChangesAsync();
            return resource;
        }

        public async Task<TodoItem> UpdateAsync(TodoItem resource)
        {
            _context.Update(resource);
            await _context.SaveChangesAsync();
            return resource;
        }

        public async Task DeleteAsync(string id)
        {
            var task = await GetByIDAsync(id);

            if (task is null)
            {
                return;
            }

            _context.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
