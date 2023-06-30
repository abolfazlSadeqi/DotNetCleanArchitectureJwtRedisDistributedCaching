using Common;
using Common.Validation;
using FluentValidation;
using MediatR;


namespace Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(v => v.Firstname)
            .MinimumLength(10).WithMessage(string.Format(ErrorMessage.MinimumLength, "Firstname"))
            .MaximumLength(100).WithMessage(string.Format(ErrorMessage.MaximumLength, "Firstname"))
            .NotEmpty().WithMessage(string.Format(ErrorMessage.NotEmpty, "Firstname"));

        RuleFor(v => v.Lastname)
            .MinimumLength(10).WithMessage(string.Format(ErrorMessage.MinimumLength, "Lastname"))
            .MaximumLength(100).WithMessage(string.Format(ErrorMessage.MaximumLength, "Lastname"))
            .NotEmpty().WithMessage(string.Format(ErrorMessage.NotEmpty, "Lastname"));


        RuleFor(v => v.PhoneNumber)
          .MinimumLength(10).WithMessage(string.Format(ErrorMessage.MinimumLength, "PhoneNumber"))
            .MaximumLength(20).WithMessage(string.Format(ErrorMessage.MaximumLength, "PhoneNumber"))
             .NotEmpty().WithMessage(string.Format(ErrorMessage.NotEmpty, "PhoneNumber"))
             .Must(d => Validatedata.IsValidphoneNumber(d)).WithMessage(ErrorMessage.PhoneNumberNotValid);


        RuleFor(v => v.Email)
        .MinimumLength(10).WithMessage(string.Format(ErrorMessage.MinimumLength, "Email"))
            .MaximumLength(100).WithMessage(string.Format(ErrorMessage.MaximumLength, "Email"))
          .NotEmpty().WithMessage(string.Format(ErrorMessage.NotEmpty, "Email"))
       .Must(d => Validatedata.IsValidEmailValue(d)).WithMessage(ErrorMessage.EmailNotValid);




        RuleFor(v => v.BankAccountNumber)
         .MinimumLength(10).WithMessage(string.Format(ErrorMessage.MinimumLength, "BankAccountNumber"))
           .MaximumLength(20).WithMessage(string.Format(ErrorMessage.MaximumLength, "BankAccountNumber"))
          .NotEmpty().WithMessage(string.Format(ErrorMessage.NotEmpty, "BankAccountNumber"))
         .Must(dd => Validatedata.IsValidValidateBankAccountNumber(dd))
         .WithMessage(ErrorMessage.BankAccountNumberNotValid);



    }
}
