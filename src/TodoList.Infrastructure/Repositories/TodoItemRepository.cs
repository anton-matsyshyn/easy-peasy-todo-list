using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Application.Interfaces.DataProxies;
using TodoList.Core.Common;
using TodoList.Infrastructure.EntityFramework.Contexts;
using TodoList.Infrastructure.EntityFramework.Entities;

namespace TodoList.Infrastructure.Repositories
{
    internal class TodoItemRepository : ITodoItemDataProxy
    {
        private readonly TodoListDbContext _context;
        private readonly IMapper _mapper;

        public TodoItemRepository(TodoListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            var entities = await _context.Items.ToArrayAsync();
            return _mapper.Map<IEnumerable<TodoItem>>(entities);
        }

        public async Task<TodoItem> GetByIDAsync(string id)
        {
            var entity = await _context.Items.SingleOrDefaultAsync(_ => _.ID == id);

            if (entity is null)
            {
                return null;
            }

            return _mapper.Map<TodoItem>(entity);
        }

        public async Task<TodoItem> InsertAsync(TodoItem resource)
        {
            var entity = _mapper.Map<TodoItemEntity>(resource);
           
            _context.Add(entity);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<TodoItem>(entity);
        }

        public async Task<TodoItem> UpdateAsync(TodoItem resource)
        {
            var entity = await _context.Items.SingleOrDefaultAsync(_ => _.ID == resource.ID);
            entity = _mapper.Map(resource, entity);
            
            _context.Update(entity);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<TodoItem>(entity);
        }

        public async Task DeleteAsync(string id)
        {
            var item = await _context.Items.SingleOrDefaultAsync(_ => _.ID == id);

            if (item is null)
            {
                return;
            }

            _context.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
