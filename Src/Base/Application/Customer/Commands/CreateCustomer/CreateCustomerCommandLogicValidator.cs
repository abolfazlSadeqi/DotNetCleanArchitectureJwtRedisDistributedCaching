
using Application.Common.Interfaces;
using Common;
using Common.Exceptions;
using Domain.Entities;

namespace Application.Customers.Commands.CreateCustomer;

public class UpdateCustomerCommandLogicValidator
{

    public void IsValidEmailunique(IApplicationDbContext context, Customer customer)
    {
        //Email must be unique in the database.
        var item = context.Customers.FirstOrDefault(d => d.Email == customer.Email);
        if ((item != null && item.Id != customer.Id)) 
            throw new ValidationLogicException(new List<Exception>() { new Exception(ErrorMessage.ValidEmailunique)});


    }

    public void IsValidCustomerslunique(IApplicationDbContext context, Customer customer)
    {
        //Customers must be unique in database: By Firstname, Lastname and DateOfBirth.

        var item = context.Customers.FirstOrDefault(d => d.Firstname == customer.Firstname && d.Lastname == customer.Lastname
        && d.DateOfBirth == customer.DateOfBirth);

        IEnumerable<FluentValidation.Results.ValidationFailure> errors;

        if ((item != null && item.Id != customer.Id)) 
            throw new  ValidationLogicException(new List<Exception>() { new Exception(ErrorMessage.ValidCustomersunique)});
    }
    public void IsValidCustomers(CreateCustomerCommand request)
    {
        CreateCustomerCommandValidator validations = new CreateCustomerCommandValidator();
        var _result = validations.Validate(request);
        if (_result is not null && !_result.IsValid && _result.Errors.Count() > 0) throw new ValidationException(_result.Errors);
    }


}