using System.Linq.Expressions;
using Dummy.Data;
using Dummy.Data.Entities;
using EntityGraphQL.Schema;

namespace Dummy.API.Mutations;

public class EditOrderArgs
{
    public string title { get; set; }
    public int quantity { get; set; }
    public int id { get; set; }
}

public class EditOrderMutation(DummyDbContext context) : IMutation
{
    public EditOrderMutation() : this(null)
    {
    }

    [GraphQLMutation("Edit an order")]
    public Expression<Func<DummyDbContext, Order>> EditOrder([GraphQLArguments] EditOrderArgs input)
    {
        var order = context.Orders.First(x => x.Id == input.id);
        order.Title = input.title;
        order.Quantity = -1;
        context.SaveChanges();

        return ctx => ctx.Orders.First(x => x.Id == input.id);
    }
}