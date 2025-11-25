using Dummy.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dummy.Data;

public class DummyDbContext(DbContextOptions<DummyDbContext> options) : DbContext(options)
{
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Item)
            .WithMany()
            .HasForeignKey(o => o.ItemId);
    }
}