using Microsoft.Extensions.DependencyInjection;
using TodoList.Application.Factories;
using TodoList.Application.Interfaces.Factories;
using TodoList.Application.Interfaces.Services;
using TodoList.Application.Services;

namespace TodoList.Application.Extensions
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ITodoItemService, TodoItemService>();
            services.AddTransient<IToDoListService, ToDoListService>();

            services.AddTransient<IRuleFactory, RuleFactory>();

            return services;
        }
    }
}
