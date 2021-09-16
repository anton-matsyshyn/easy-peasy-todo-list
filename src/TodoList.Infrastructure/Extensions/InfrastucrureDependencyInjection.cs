using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using TodoList.Application.Interfaces;
using TodoList.Application.Interfaces.DataProxies;
using TodoList.Infrastructure.EntityFramework.Contexts;
using TodoList.Infrastructure.Mocks;
using TodoList.Infrastructure.Repositories;
using TodoList.Infrastructure.Services;

namespace TodoList.Infrastructure.Extensions
{
    public static class InfrastucrureDependencyInjection
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

            services.AddScoped<IToDoListDataProxy, ToDoListRepository>();
            services.AddScoped<ITodoItemDataProxy, TodoItemRepository>();
            services.AddScoped<IUserDataProxy, UserRepository>();

            services.AddScoped<ICurrentUserContext, CurrentUserContext>();

            services.AddTransient<INotificationSender, NotificationSender>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.ApplySeeding();

            return services;
        }

        private static IServiceCollection ApplySeeding(this IServiceCollection services)
        {
            using var provider = services.BuildServiceProvider();
            var context = provider.GetService<TodoListDbContext>();

            context.Database.Migrate();

            var insertedUserIds = context.Users.Select(_ => _.ID).ToArray();
            var missingUsers = UserMock.Users.Where(_ => !insertedUserIds.Contains(_.ID));
            context.Users.AddRange(missingUsers);

            var insertedListIds = context.Lists.Select(_ => _.ID).ToArray();
            var missingLists = ListMock.Lists.Where(_ => !insertedListIds.Contains(_.ID));
            context.Lists.AddRange(missingLists);

            var insertedItemIds = context.Items.Select(_ => _.ID).ToArray();
            var missingItems = ItemsMock.Items.Where(_ => !insertedItemIds.Contains(_.ID));
            context.Items.AddRange(missingItems);

            context.SaveChanges();

            return services;
        }
    }
}
