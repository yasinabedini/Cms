using Cms.Domain.Common.Event;

namespace Cms.Domain.Common.Entities
{
    public interface IAggregateRoot
    {
        void ClearEvents();
        IEnumerable<IDomainEvent> GetEvents();
    }
}