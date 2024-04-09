using Cms.Domain.Common.Event;

namespace Cmd.Application.Common.Entities
{
    public interface IAggregateRoot
    {
        void ClearEvents();
        IEnumerable<IDomainEvent> GetEvents();
    }
}