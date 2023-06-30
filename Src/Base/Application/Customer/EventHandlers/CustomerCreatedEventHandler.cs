using Application.Common.Models;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Customers.EventHandlers;

public class CustomerCreatedEventHandler : INotificationHandler<DomainEventNotification<CustomerCreatedEvent>>
{
    private readonly ILogger<CustomerCreatedEventHandler> _logger;

    public CustomerCreatedEventHandler(ILogger<CustomerCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<CustomerCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}