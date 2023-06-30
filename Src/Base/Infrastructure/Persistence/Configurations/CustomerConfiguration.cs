using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");

        builder.Ignore(e => e.DomainEvents);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        //builder.Property(p => p.RowVersion)
        //   .IsConcurrencyToken()
        //   .ValueGeneratedOnAddOrUpdate();

        builder.Property(e => e.Firstname)
            .HasMaxLength(100).HasColumnType("VARCHAR(100)");

        builder.Property(e => e.Lastname)
            .HasMaxLength(100).HasColumnType("VARCHAR(100)");

        builder.Property(e => e.DateOfBirth)
          .HasMaxLength(100).HasColumnType("date");


        builder.Property(e => e.PhoneNumber)
         .HasMaxLength(20).HasColumnType("VARCHAR(20)");


        builder.Property(e => e.Email)
       .HasMaxLength(100).HasColumnType("VARCHAR(100)");

        builder.Property(e => e.BankAccountNumber)
       .HasMaxLength(20).HasColumnType("VARCHAR(20)");
    }
}
