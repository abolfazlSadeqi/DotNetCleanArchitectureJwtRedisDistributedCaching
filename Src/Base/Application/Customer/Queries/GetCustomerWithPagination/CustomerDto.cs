using Application.Common.Mappings;
using Domain.Entities;
using Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Application.Customers.Queries.GetCustomeresWithPagination;

public class CustomerDto : IMapFrom<Customer>
{
   
    public int Id { get; set; }
    public string Firstname { get; set; }

   public string Lastname { get; set; }

    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }

    public string Email { get; set; }


    public string BankAccountNumber { get; set; }

    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public Guid RowVersion { get; set; }
}
