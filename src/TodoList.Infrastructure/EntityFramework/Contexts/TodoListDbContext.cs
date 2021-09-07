using Microsoft.EntityFrameworkCore;
using TodoList.Core.Common;

namespace TodoList.Infrastructure.EntityFramework.Contexts
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options): base(options)
        { }

        public DbSet<TodoItem> Items { get; set; }
        public DbSet<ToDoList> Lists { get; set; }
    }
}
