using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Application.Interfaces.DataProxies;
using TodoList.Core.Common;
using TodoList.Infrastructure.EntityFramework.Contexts;
using TodoList.Infrastructure.EntityFramework.Entities;

namespace TodoList.Infrastructure.Repositories
{
    internal class ToDoListRepository : IToDoListDataProxy
    {
        private readonly TodoListDbContext _context;
        private readonly IMapper _mapper;

        public ToDoListRepository(TodoListDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ToDoList>> GetAllAsync()
        {
            var lists = await _context.Lists.ToArrayAsync();
            return _mapper.Map<IEnumerable<ToDoList>>(lists);
        }

        public async Task<ToDoList> GetByIDAsync(string id)
        {
            var list = await _context.Lists.SingleOrDefaultAsync(_ => _.ID == id);

            if (list is null)
            {
                return null;
            }

            return _mapper.Map<ToDoList>(list);
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItems(string listId)
        {
            var items = await _context.Items.Where(_ => _.ListId == listId).ToArrayAsync();
            return _mapper.Map<IEnumerable<TodoItem>>(items);
        }

        public async Task<ToDoList> InsertAsync(ToDoList resource)
        {
            var entity = _mapper.Map<ToDoListEntity>(resource);
            
            _context.Add(entity);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<ToDoList>(entity);
        }

        public async Task<ToDoList> UpdateAsync(ToDoList resource)
        {
            var entity = await _context.Lists.SingleOrDefaultAsync(_ => _.ID == resource.ID);
            entity = _mapper.Map(resource, entity);
            
            _context.Update(entity);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<ToDoList>(entity);
        }

        public async Task DeleteAsync(string id)
        {
            var list = await _context.Lists.SingleOrDefaultAsync(_ => _.ID == id);

            if (list is null)
            {
                return;
            }

            _context.Remove(list);
            await _context.SaveChangesAsync();
        }
    
        public async Task<bool> CheckIfUserAssociatedWithList(string userId, string listId)
        {
            var list = await GetByIDAsync(listId);

            if (list == null)
            {
                return false;
            }

            return list.CreatorId == userId;
        }
    }
}
