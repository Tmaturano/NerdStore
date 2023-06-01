using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NS.Basket.API.Models;
using NS.Core.Data;

namespace NS.Basket.API.Data;

public class BasketContext : DbContext, IUnitOfWork
{
    public BasketContext(DbContextOptions<BasketContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<BasketClient> BasketClients { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.Ignore<ValidationResult>();

        modelBuilder.Entity<BasketClient>()
            .HasIndex(c => c.ClientId)
            .HasDatabaseName("IDX_Client");

        modelBuilder.Entity<BasketClient>()
            .HasMany(c => c.Items)
            .WithOne(i => i.BasketClient)
            .HasForeignKey(c => c.BasketId);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
    }

    public async Task<bool> CommitAsync() => await base.SaveChangesAsync() > 0;
}
