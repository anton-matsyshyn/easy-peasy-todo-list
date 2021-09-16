using Microsoft.EntityFrameworkCore;
using TodoList.Infrastructure.EntityFramework.Entities;

namespace TodoList.Infrastructure.EntityFramework.Contexts
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options): base(options)
        { }

        public DbSet<TodoItemEntity> Items { get; set; }
        public DbSet<ToDoListEntity> Lists { get; set; }
        public DbSet<UserEntity> Users { get; set; }
    }
}
