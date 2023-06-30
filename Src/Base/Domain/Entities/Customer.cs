using Domain.Common;
using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Common.Validation;
using Common.Other;
using System.Runtime.CompilerServices;

namespace Domain.Entities;

public class Customer : AuditableEntity, IHasDomainEvent
{

    public Customer CreateCustomer(
        string firstname,
        string lastname,
        DateTime dateOfBirth
        , string phoneNumber,
         string email, 
         string bankAccountNumber
        )
    {
        Customer customer = new();
        customer.Firstname = firstname;
        customer.Lastname = lastname;
        customer.DateOfBirth = dateOfBirth;
        customer.PhoneNumber = phoneNumber;
        customer.Email = email;
        customer.BankAccountNumber = bankAccountNumber;
        return customer;
    }


    public Customer UpdateCustomer( Customer _Customer,
        string firstname,
        string lastname,
        DateTime dateOfBirth
        , string phoneNumber,
         string email,
         string bankAccountNumber
        )
    {
        _Customer.Firstname = firstname;
        _Customer.Lastname = lastname;
        _Customer.DateOfBirth = dateOfBirth;
        _Customer.PhoneNumber = phoneNumber;
        _Customer.Email = email;
        _Customer.BankAccountNumber = bankAccountNumber;
        return _Customer;
    }


    public int Id { get; set; }


    public string Firstname { get; set; }



    public string Lastname { get; set; }

    public DateTime DateOfBirth { get; set; }



    public string PhoneNumber { get; set; }


    public string Email { get; set; }

    public string BankAccountNumber { get; set; }



    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}