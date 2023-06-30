using Common.Exceptions;
using Application.Common.Interfaces;
using Application.Customers.Commands.CreateCustomer;
using Domain.Entities;
using Domain.Events;
using Domain.ValueObjects;
using MediatR;

namespace Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommand : IRequest<int>
{
    public int Id { get; set; }

    public string Firstname { get; set; }


    public string Lastname { get; set; }

    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }



    public string Email { get; set; }


    public string BankAccountNumber { get; set; }
}

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand,int>
{
    private readonly IApplicationDbContext _context;

    public UpdateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }


  
    public async Task<int> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {


        UpdateCustomerCommandLogicValidator validationsLogic = new UpdateCustomerCommandLogicValidator();
        validationsLogic.IsValidCustomers(request);


        var entity = await _context.Customers
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        entity.UpdateCustomer(entity,

             request.Firstname, request.Lastname, request.DateOfBirth,
             request.PhoneNumber, request.Email, request.BankAccountNumber
          );

        validationsLogic.IsValidEmailunique(_context, entity);
        validationsLogic.IsValidCustomerslunique(_context, entity);


        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

