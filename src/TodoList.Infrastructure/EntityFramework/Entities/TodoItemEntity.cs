using System;
using TodoList.Core.Enums;

namespace TodoList.Infrastructure.EntityFramework.Entities
{
    public class TodoItemEntity
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ListId { get; set; }
        public TaskType Type { get; set; }
        public TaskComplexity Complexity { get; set; }

        public ToDoListEntity List { get; set; }
    }
}
