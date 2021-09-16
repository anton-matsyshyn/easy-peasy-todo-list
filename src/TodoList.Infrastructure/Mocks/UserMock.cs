using System;
using System.Collections.Generic;
using TodoList.Infrastructure.EntityFramework.Entities;

namespace TodoList.Infrastructure.Mocks
{
    internal static class UserMock
    {
        internal static List<UserEntity> Users = new List<UserEntity>
        {
            new UserEntity
            {
                ID = "e2526259-c5f3-497a-93da-83e653907d65",
                BirthDate = new DateTime(1998, 10, 14),
                FirstName = "Svetlana",
                LastName = "Diode",
                Email = "sveta.diode@gmail.com"
            },
            new UserEntity
            {
                ID = "1488ea2e-c920-4e66-8009-7a41a427e082",
                BirthDate = new DateTime(1997, 9, 11),
                FirstName = "Alyona",
                LastName = "Transistor",
                Email = "alyona.polar.transistor@gmail.com"
            },
            new UserEntity
            {
                ID = "8cab6209-c37b-40c1-9417-4a9d1d469bc8",
                BirthDate = new DateTime(2001, 11, 11),
                FirstName = "Misha",
                LastName = "Google Chrome",
                Email = "misha.chrome@gmail.com"
            },
        };
    }
}
