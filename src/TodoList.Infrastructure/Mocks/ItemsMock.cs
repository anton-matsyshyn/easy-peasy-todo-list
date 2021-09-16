using System;
using System.Collections.Generic;
using TodoList.Core.Enums;
using TodoList.Infrastructure.EntityFramework.Entities;

namespace TodoList.Infrastructure.Mocks
{
    internal class ItemsMock
    {
        internal static List<TodoItemEntity> Items = new List<TodoItemEntity>
        {
            new TodoItemEntity
            {
                ID = "255c01a6-1708-4fa0-8bdd-7d45793737e5",
                Title = "Go through Docker getting started",
                Complexity = TaskComplexity.Easy,
                List = ListMock.Lists[0],
                Type = TaskType.Work,
            },
            new TodoItemEntity
            {
                ID = "e0f23b65-09df-47c1-ad2d-6be55d630ac2",
                Title = "Get AWS certificate",
                Complexity = TaskComplexity.Middle,
                List = ListMock.Lists[0],
                Type = TaskType.Work, 
            },
            new TodoItemEntity
            {
                ID = "abff7e38-e555-4871-94a1-f18cccdc1ef9",
                Title = "Deploy docker image to AWS VM",
                Complexity = TaskComplexity.Middle,
                List = ListMock.Lists[0],
                Type = TaskType.Work,
            },
            new TodoItemEntity
            {
                ID = "12478ce4-5017-49c5-9dce-adbca1f217cf",
                Title = "The Idiot",
                Complexity = TaskComplexity.NotSet,
                List = ListMock.Lists[1],
                Type = TaskType.SelfDevelopment,
            },
            new TodoItemEntity
            {
                ID = "f69946f1-1ed0-4bd7-81ac-76307adcb9cf",
                Title = "Crime and Punishment",
                Complexity = TaskComplexity.NotSet,
                List = ListMock.Lists[1],
                Type = TaskType.SelfDevelopment,
            },
            new TodoItemEntity
            {
                ID = "f69946f1-1ed0-4bd7-81ac-76307adcb9cd",
                Title = "The Brothers Karamazov",
                Complexity = TaskComplexity.NotSet,
                List = ListMock.Lists[1],
                Type = TaskType.SelfDevelopment,
            },
        };
    }
}
