using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dummy.Data;
using EntityGraphQL;
using EntityGraphQL.Schema;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Dummy.API.Endpoints;

public class GraphQlEndpont(DummyDbContext db, SchemaProvider<DummyDbContext> schema, IServiceProvider serviceProvider)
{
    private static async Task<T?> GetJsonBody<T>(HttpRequestData req)
    {
        using var reader = new StreamReader(req.Body);

        var body = await reader.ReadToEndAsync();

        if (string.IsNullOrWhiteSpace(body))
            return default;

        return JsonSerializer.Deserialize<T>(body, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }


    [Function(nameof(GraphQl))]
    public async Task<HttpResponseData> GraphQl([HttpTrigger(AuthorizationLevel.Function, "post", Route = "graphql")] HttpRequestData req)
    {
        var query = await GetJsonBody<QueryRequest>(req);
        var debug = db.Items.ToList();
        var results = await schema.ExecuteRequestAsync(query, db, serviceProvider, null);

        var response = req.CreateResponse(HttpStatusCode.OK);

        var jsonSerializerSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.None,
            Formatting = Formatting.None
        };

        JsonConvert.DefaultSettings = () => jsonSerializerSettings;
        var json = JsonConvert.SerializeObject(results, jsonSerializerSettings);


        await response.WriteStringAsync(json);
        return response;
    }
}