using Peasy;

namespace TodoList.Core.Common.Abstractions
{
    public abstract class Entity : IDomainObject<string>
    {
        public string ID { get; set; }
    }
}
