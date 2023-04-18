using Microsoft.EntityFrameworkCore;
using NS.Clients.API.Models;
using NS.Core.Data;

namespace NS.Clients.API.Data;

public class ClientContext : DbContext, IUnitOfWork
{
    public ClientContext(DbContextOptions<ClientContext> options) : base(options)
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

    public async Task<bool> CommitAsync() => await base.SaveChangesAsync() > 0;
}
