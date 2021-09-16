using System;
using System.Collections.Generic;
using TodoList.Infrastructure.EntityFramework.Entities;

namespace TodoList.Infrastructure.Mocks
{
    internal static class ListMock
    {
        internal static List<ToDoListEntity> Lists = new List<ToDoListEntity>
        {
            new ToDoListEntity
            {
                ID = "006bcde7-8337-42b8-b40d-994d70a8e22e",
                Description = "DevOps experience",
                Creator = UserMock.Users[0],
                StartDate = DateTime.Now,
                Deadline = DateTime.Now.AddDays(5),
            },
            new ToDoListEntity
            {
                ID = "2d52fb44-14ba-4eb9-97a5-b5ab360496e5",
                Description = "Read 10 Dostoevskys books",
                Creator = UserMock.Users[0],
                StartDate = DateTime.Now.AddDays(1),
                Deadline = DateTime.Now.AddDays(14),
            },
            new ToDoListEntity
            {
                ID = "e21c0f77-8468-4fdc-9927-2625aa53a6ed",
                Description = "Watch films in native dub",
                Creator = UserMock.Users[1],
                StartDate = DateTime.Now.AddDays(-5),
                Deadline = DateTime.Now.AddDays(30),
            },
        };
    }
}
