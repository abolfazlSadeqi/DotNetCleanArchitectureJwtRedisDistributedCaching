using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Infrastructure.Persistence;



public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>, IApplicationDbContext
{
    private readonly IDateTime _dateTime;
    private readonly IDomainEventService _domainEventService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IDateTime dateTime,
        IDomainEventService domainEventService) : base(options)
    {
        _dateTime = dateTime;
        _domainEventService = domainEventService;

    }

   
    public DbSet<Customer> Customers => Set<Customer>();
   

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = "UserId";
                    entry.Entity.Created = _dateTime.Now;
                    entry.Entity.RowVersion = Guid.NewGuid();
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = "UserId";
                    entry.Entity.LastModified = _dateTime.Now;
                    entry.Entity.RowVersion = Guid.NewGuid();

                    break;
            }
        }

        var events = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToArray();

        var result = 0;

        try
        {
        
            result = await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
           
            ex.Entries.Single().Reload();
        }

        await DispatchEvents(events);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>()
           .ToTable("Users");

        modelBuilder.Entity<ApplicationRole>().ToTable("Roles");
        modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRoles");
        modelBuilder.Entity<ApplicationUserLogin>().ToTable("UserLogins");
        modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaims");
        modelBuilder.Entity<ApplicationRoleClaim>().ToTable("RoleClaims");
        modelBuilder.Entity<ApplicationUserToken>().ToTable("UserTokens");

    }

    private async Task DispatchEvents(DomainEvent[] events)
    {
        foreach (var @event in events)
        {
            @event.IsPublished = true;
            await _domainEventService.Publish(@event);
        }
    }
}
