using System;
using System.Collections.Generic;
using TodoList.Core.Common.Abstractions;

namespace TodoList.Core.Common
{
    public class TodoList : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? FinishDate { get; set; }

        public List<Task> Items { get; set; }
    }
}
