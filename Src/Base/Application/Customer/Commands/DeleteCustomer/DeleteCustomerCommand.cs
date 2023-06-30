
using Application.Common.Interfaces;
using Common.Exceptions;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        _context.Customers.Remove(entity);

        entity.DomainEvents.Add(new CustomerDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}