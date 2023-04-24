using Microsoft.EntityFrameworkCore;
using NS.Clients.API.Models;
using NS.Core.Data;
using NS.Core.DomainObjects;
using NS.Core.Mediator;

namespace NS.Clients.API.Data;

public class ClientContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;
    public ClientContext(DbContextOptions<ClientContext> options, IMediatorHandler mediatorHandler) : base(options)
    {
        //This architecture does not depend of these features
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");
                
        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientContext).Assembly);
    }

    public async Task<bool> CommitAsync()
    {
        var success = await base.SaveChangesAsync() > 0;
        if (success) await _mediatorHandler.PublishEvents(this);

        return success;
    }
}

/// <summary>
/// Extension to send mediator events for every class that exents Entity type
/// </summary>
public static class MediatorExtension
{
    public static async Task PublishEvents<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notifications is not null && x.Entity.Notifications.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notifications)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearEvents());

        var tasks = domainEvents
            .Select(async (domainEvent) => {
                await mediator.PublishEvent(domainEvent);
            });

        await Task.WhenAll(tasks);
    }
}
