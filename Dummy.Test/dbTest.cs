using System.Collections;
using Dummy.Data;
using Dummy.Data.Entities;
using Dummy.Test;
using Dummy.Tests.TestHelpers;
using EntityGraphQL;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

[TestClass]
public class DbTests
{
    [TestMethod]
    public void ItemsSeededCorrectly()
    {
        using var db = TestDbFactory.CreateContext();
        Assert.AreEqual(2, db.Items.Count());
    }

    [TestMethod]
    public void CanQueryCorrectly()
    {
        using var db = TestDbFactory.CreateContext();
        var schema = GraphQLTestHelper.CreateSchema();

        var request = new QueryRequest
        {
            Query =
                @"query {
                      items {
                        id
                        name
                        quantity
                      }
                    }"
        };

        var result = schema.ExecuteRequest(request, db, null, null, null);

        //There should be two items
        var itemsObj = result.Data["items"];
        var items = ((IEnumerable<object>)itemsObj).Cast<dynamic>();
        Assert.AreEqual(2, items.Count());

        //And we're asking for 3 fields
        var firstItem = Enumerable.ElementAt((dynamic)result.Data["items"]!, 0);
        Assert.AreEqual(3, firstItem.GetType().GetFields().Length);

        //Of which all fields should have a value
        Assert.IsNotNull(firstItem.id);
        Assert.IsNotNull(firstItem.name);
        Assert.IsNotNull(firstItem.quantity);
    }


    [TestMethod]
    public void CanMutateCorrectly()
    {
        var (db, provider) = TestDbFactory.CreateContextWithDI();
        var schema = GraphQLTestHelper.CreateSchema();

        var request = new QueryRequest
        {
            Query = @"
                mutation($title: String!, $quantity: Int!) {
                  createOrder (title: $title, quantity: $quantity) {
                    id
                    quantity
                    title
                  }
                }",
            Variables = new QueryVariables { { "title", "Metallica" }, { "quantity", "1" } },
        };

        var result = schema.ExecuteRequest(request, db, provider, null, null);


        var order = (dynamic)result.Data["createOrder"];
        
        //We asked for 3 fields
        Assert.AreEqual(3, order.GetType().GetFields().Length);
        
        //Of which they should be...
        Assert.IsNotNull(order.id);
        Assert.AreEqual(order.quantity, 1);
        Assert.AreEqual(order.title, "Metallica");
    }
}