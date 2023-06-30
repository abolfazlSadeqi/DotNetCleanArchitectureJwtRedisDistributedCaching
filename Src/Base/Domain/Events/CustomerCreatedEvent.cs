using Domain.Common;
using Domain.Entities;

namespace Domain.Events;

public class CustomerCreatedEvent : DomainEvent
{
    public CustomerCreatedEvent(Customer item)
    {
        Item = item;
    }

    public Customer Item { get; }
}