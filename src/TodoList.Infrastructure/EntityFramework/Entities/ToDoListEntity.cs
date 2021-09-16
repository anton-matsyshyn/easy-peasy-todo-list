using System;
using System.Collections.Generic;

namespace TodoList.Infrastructure.EntityFramework.Entities
{
    public class ToDoListEntity
    {
        public string ID { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? FinishDate { get; set; }
        public string CreatorId { get; set; }

        public IList<TodoItemEntity> Items { get; set; }
        public UserEntity Creator { get; set; }
    }
}
