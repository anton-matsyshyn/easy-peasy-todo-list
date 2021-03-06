using Peasy;
using System;
using TodoList.Core.Enums;

namespace TodoList.Core.Common
{
    public class TodoItem : IDomainObject<string>
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ListId { get; set; }
        public DateTime Deadline { get; set; }
        public TaskType Type { get; set; }
        public TaskComplexity Complexity { get; set; }
    }
}
