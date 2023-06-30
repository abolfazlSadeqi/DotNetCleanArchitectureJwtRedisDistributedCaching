using Domain.Common;
using Domain.Entities;

namespace Domain.Events;

public class CustomerDeletedEvent : DomainEvent
{
    public CustomerDeletedEvent(Customer item)
    {
        Item = item;
    }

    public Customer Item { get; }
}
