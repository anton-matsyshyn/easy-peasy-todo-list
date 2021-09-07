using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Infrastructure.EntityFramework.Contexts;
using TodoList.Infrastructure.EntityFramework.Interfaces;
using TodoList.Infrastructure.EntityFramework.Repositories;

namespace TodoList.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
                this IServiceCollection services,
                IConfiguration configuration
            )
        {
            services.AddDbContext<TodoListDbContext>(
                options => options.UseSqlServer(
                    configuration["ConnectionStrings:SqlServer"],
                    b => b.MigrationsAssembly(typeof(TodoListDbContext).Assembly.FullName)
                )
            );

            services.AddScoped<IToDoListRepository, ToDoListRepository>();
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();

            return services;
        }
    }
}
