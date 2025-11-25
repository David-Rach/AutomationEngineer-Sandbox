using Dummy.API.Mutations;
using Dummy.Data;
using Dummy.Data.Entities;
using EntityGraphQL.Schema;

namespace Dummy.Tests.TestHelpers;

public static class GraphQLTestHelper
{
    public static SchemaProvider<DummyDbContext> CreateSchema()
    {
        var schema = SchemaBuilder.FromObject<DummyDbContext>();
        
        schema.AddMutationsFrom<IMutation>(new SchemaBuilderMethodOptions()
        {
            AutoCreateEnumTypes = true,
            AutoCreateInputTypes = true,
            AutoCreateNewComplexTypes = true,
            AutoCreateInterfaceTypes = true
        });
        return schema;
    }
}