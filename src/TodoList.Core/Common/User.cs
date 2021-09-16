using Peasy;
using System;

namespace TodoList.Core.Common
{
    public class User : IDomainObject<string>
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
