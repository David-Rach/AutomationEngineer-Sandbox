using Dummy.Data;
using Inventory.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dummy.Api.Services;

public class DbInitializerHostedService(IServiceProvider provider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = provider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DummyDbContext>();
        await DataSeeder.SeedAsync(db);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}