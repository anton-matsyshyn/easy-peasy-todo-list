using AutoMapper;
using TodoList.Core.Common;
using TodoList.Infrastructure.EntityFramework.Entities;

namespace TodoList.Infrastructure.EntityFramework.Profiles
{
    public class ToDoListProfile : Profile
    {
        public ToDoListProfile()
        {
            CreateMap<ToDoListEntity, ToDoList>()
                .ReverseMap();
        }
    }
}
