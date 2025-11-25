using System.Linq.Expressions;
using Dummy.Data;
using Dummy.Data.Entities;
using EntityGraphQL.Schema;

namespace Dummy.API.Mutations;

public class DeleteOrderMutation(DummyDbContext context) : IMutation
{
    public DeleteOrderMutation() : this(null)
    {
    }

    [GraphQLMutation("Delete an order")]
    public Expression<Func<DummyDbContext, Order?>> DeleteOrder(int orderId)
    {
        var orderToRemove = context.Orders.First(x => x.Id == orderId);
        context.Orders.Remove(orderToRemove);
        context.SaveChanges();
        return ctx => null;
    }
}