using Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using Domain.ValueObjects;
using Common.Other;
using MediatR;

namespace Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<int>
{

    public string Firstname { get; set; }



    public string Lastname { get; set; }

    public DateTime DateOfBirth { get; set; }



    public string PhoneNumber { get; set; }



    public string Email { get; set; }


    public string BankAccountNumber { get; set; }
}

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        UpdateCustomerCommandLogicValidator validationsLogic = new UpdateCustomerCommandLogicValidator();
        validationsLogic.IsValidCustomers(request);

        Customer entity = new();
        entity = entity.CreateCustomer(

           request.Firstname, request.Lastname, request.DateOfBirth,
           request.PhoneNumber, request.Email, request.BankAccountNumber
        );

        validationsLogic.IsValidEmailunique(_context, entity);
        validationsLogic.IsValidCustomerslunique(_context, entity);


        entity.DomainEvents.Add(new CustomerCreatedEvent(entity));

        _context.Customers.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}