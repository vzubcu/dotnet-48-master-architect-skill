// EF6 Compiled Query for Hot Paths
private static readonly Func<MyContext, int, Order> GetOrderByIdQuery =
    EF.CompileQuery((MyContext ctx, int id) =>
        ctx.Orders
            .Include(o => o.Items)
            .Include(o => o.Customer)
            .AsNoTracking()
            .FirstOrDefault(o => o.Id == id));

public Order GetOrderFast(int id)
{
    using (var ctx = new MyContext())
    {
        return GetOrderByIdQuery(ctx, id);
    }
}
