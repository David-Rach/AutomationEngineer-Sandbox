using Dummy.Data;
using Dummy.Data.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dummy.Tests.TestHelpers;

public static class TestDbFactory
{
    public static DummyDbContext CreateContext()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<DummyDbContext>()
            .UseSqlite(connection)
            .EnableSensitiveDataLogging()
            .Options;

        var db = new DummyDbContext(options);
        db.Database.EnsureCreated();

        // optional seed
        db.Items.AddRange(
            new Item { Name = "Widget", Quantity = 10 },
            new Item { Name = "Gadget", Quantity = 5 }
        );
        db.SaveChanges();

        return db;
    }
    
    public static (DummyDbContext db, IServiceProvider provider) CreateContextWithDI()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<DummyDbContext>()
            .UseSqlite(connection)
            .Options;

        var db = new DummyDbContext(options);
        db.Database.EnsureCreated();
        
        // optional seed
        db.Items.AddRange(
            new Item { Name = "Widget", Quantity = 10 },
            new Item { Name = "Gadget", Quantity = 5 }
        );
        db.SaveChanges();

        var services = new ServiceCollection();
        services.AddDbContext<DummyDbContext>(o => o.UseSqlite(connection));
        
        services.AddScoped<IOrderValidator, OrderValidator>();

        var provider = services.BuildServiceProvider();

        return (db, provider);
    }

}