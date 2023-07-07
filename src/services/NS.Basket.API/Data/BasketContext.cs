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
            .Ignore(c => c.Voucher) //this property will not be a column in the table
            .OwnsOne(c => c.Voucher, v =>
            {
                v.Property(vc => vc.Code)
                    .HasColumnName("VoucherCode")
                    .HasColumnType("varchar(50)");

                v.Property(vc => vc.DiscountType)
                    .HasColumnName("DiscountType");

                v.Property(vc => vc.Percentage)
                    .HasColumnName("Percentage");

                v.Property(vc => vc.DiscountValue)
                    .HasColumnName("DiscountValue");
            });

        modelBuilder.Entity<BasketClient>()
            .HasMany(c => c.Items)
            .WithOne(i => i.BasketClient)
            .HasForeignKey(c => c.BasketId);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
    }

    public async Task<bool> CommitAsync() => await base.SaveChangesAsync() > 0;
}
