using System.Linq.Expressions;
using Dummy.Data;
using Dummy.Data.Entities;
using EntityGraphQL.Schema;

namespace Dummy.API.Mutations;

public class CreateOrderArgs
{
    public string title { get; set; }
    public int quantity { get; set; }
}

public class CreateOrderMutation(DummyDbContext context, IOrderValidator orderValidator) : IMutation
{
    //Parameterless constructor to make graphQL happy
    public CreateOrderMutation() : this(null, null)
    {
    }

    [GraphQLMutation("Add an order to the system.")]
    public Expression<Func<DummyDbContext, Order>> CreateOrder([GraphQLArguments] CreateOrderArgs args)
    {
        if (!orderValidator.ValidateTitleLength(args.title))
            throw new Exception("Invalid title length");

        if (!orderValidator.ValidateWhiteSpace(args.title))
            throw new Exception("Invalid characters in title");

        var f = context.Orders.Add(new Order
        {
            Title = args.title,
            Item = new Item
            {
                Name = "Guitar picks",
                Quantity = 100
            },
            Quantity = args.quantity,
        });

        context.SaveChanges();

        return db => db.Orders.FirstOrDefault(x => x.Id == f.Entity.Id);
    }
}