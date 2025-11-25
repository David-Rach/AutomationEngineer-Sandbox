using Dummy.API.Mutations;
using Dummy.Api.Services;
using Dummy.Data;
using Dummy.Data.Entities;
using EntityGraphQL.Schema;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Services.AddDbContext<DummyDbContext>(options => { options.UseSqlite("Data Source=inventory.db"); });
builder.Services.AddHostedService<DbInitializerHostedService>();

builder.Services.AddScoped<IOrderValidator, OrderValidator>();

var schema = SchemaBuilder.FromObject<DummyDbContext>();
builder.Services.AddSingleton<SchemaProvider<DummyDbContext>>(sp =>
{
    schema.AddMutationsFrom<IMutation>(new SchemaBuilderMethodOptions()
    {
        AutoCreateEnumTypes = true,
        AutoCreateInputTypes = true,
        AutoCreateNewComplexTypes = true,
        AutoCreateInterfaceTypes = true
    });
    return schema;
});


builder.Build().Run();