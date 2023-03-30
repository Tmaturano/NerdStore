using Microsoft.EntityFrameworkCore;
using NS.Catalog.API.Models;
using NS.Core.Data;

namespace NS.Catalog.API.Data;

public class CatalogContext : DbContext, IUnitOfWork
{
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
    }

    public async Task<bool> CommitAsync() => await base.SaveChangesAsync() > 0;
}
