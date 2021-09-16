using AutoMapper;
using TodoList.Core.Common;
using TodoList.Infrastructure.EntityFramework.Entities;

namespace TodoList.Infrastructure.EntityFramework.Profiles
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItemEntity, TodoItem>()
                .ReverseMap();
        }
    }
}
