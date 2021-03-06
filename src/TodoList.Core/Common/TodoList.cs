using Peasy;
using System;

namespace TodoList.Core.Common
{
    public class ToDoList : IDomainObject<string>
    {
        public string ID { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? FinishDate { get; set; }
        public string CreatorId { get; set; }
    }
}
