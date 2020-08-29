using System;

namespace IMDb.Domain.Core.Messages
{
    public class DomainEvent : Event
    {
        public DomainEvent(Guid entityId)
        {
            EntityId = entityId;
        }
    }
}
