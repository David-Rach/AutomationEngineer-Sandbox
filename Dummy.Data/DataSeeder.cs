using Dummy.Data;
using Dummy.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(DummyDbContext db)
    {
        await db.Database.EnsureCreatedAsync();

        if (!db.Items.Any())
        {
            db.Items.AddRange(
                new Item { Name = "Widget", Quantity = 10 },
                new Item { Name = "Gadget", Quantity = 5 }
            );
        }

        await db.SaveChangesAsync();
    }
}