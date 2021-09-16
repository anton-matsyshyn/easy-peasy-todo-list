using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Application.Interfaces.DataProxies;
using TodoList.Core.Common;
using TodoList.Infrastructure.EntityFramework.Contexts;

namespace TodoList.Infrastructure.Repositories
{
    public class UserRepository : IUserDataProxy
    {
        private readonly TodoListDbContext _context;
        private readonly IMapper _mapper;

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var entities = await _context.Users.ToArrayAsync();
            return _mapper.Map<IEnumerable<User>>(entities);
        }

        public async Task<User> GetByIDAsync(string id)
        {
            var entity = await _context.Users.SingleOrDefaultAsync(_ => _.ID == id);

            if (entity is null)
            {
                return null;
            }

            return _mapper.Map<User>(entity);
        }

        public async Task<User> InsertAsync(User resource)
        {
            var entity = _mapper.Map<User>(resource);
            
            _context.Add(entity);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<User>(entity);
        }

        public async Task<User> UpdateAsync(User resource)
        {
            var entity = await _context.Users.SingleOrDefaultAsync(_ => _.ID == resource.ID);
            entity = _mapper.Map(resource, entity);
            
            _context.Update(entity);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<User>(entity);
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.Users.SingleOrDefaultAsync(_ => _.ID == id);

            if (entity is null)
            {
                return;
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
