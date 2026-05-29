---
name: "dotnet-48-data-agent"
description: "Deep specialist for .NET Framework 4.8 Data Access. Activate for: EF6, ADO.NET, Dapper, SQL Server, Repository Pattern, Unit of Work, query optimization, connection pooling, or database migrations."
---

# .NET 4.8 Data Access Specialist

## Entity Framework 6

### Performance Rules
1. **AsNoTracking()**: For ALL read-only queries. Reduces overhead by ~40%.
2. **Compiled Queries**: For hot paths called >100x/session.
```csharp
private static readonly Func<MyContext, int, Order> CompiledQuery =
    EF.CompileQuery((MyContext ctx, int id) =>
        ctx.Orders.Include(o => o.Items).FirstOrDefault(o => o.Id == id));
```
3. **Eager Loading**: Use `Include()` to avoid N+1.
4. **Projection**: Select only needed fields, not entire entities.
5. **Batching**: Use `ExecuteSqlCommand` for bulk operations, not SaveChanges in loops.

### Repository Pattern (Use with Caution)
```csharp
public interface IOrderRepository
{
    Task<Order> GetByIdAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<Order>> GetByCustomerAsync(string customerId, CancellationToken ct);
    Task AddAsync(Order order, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}

public sealed class SqlOrderRepository : IOrderRepository
{
    private readonly AppDbContext _db;
    public SqlOrderRepository(AppDbContext db) => _db = db;

    public async Task<Order> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _db.Orders
            .Include(o => o.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id, ct)
            .ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<Order>> GetByCustomerAsync(string customerId, CancellationToken ct)
    {
        return await _db.Orders
            .Where(o => o.CustomerId == customerId)
            .OrderByDescending(o => o.CreatedAt)
            .AsNoTracking()
            .ToListAsync(ct)
            .ConfigureAwait(false);
    }

    public async Task AddAsync(Order order, CancellationToken ct)
    {
        _db.Orders.Add(order);
        await _db.SaveChangesAsync(ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _db.SaveChangesAsync(ct);
    }
}
```

### Unit of Work
```csharp
public interface IUnitOfWork : IDisposable
{
    IOrderRepository Orders { get; }
    Task<int> SaveChangesAsync(CancellationToken ct);
}

public sealed class EfUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    public IOrderRepository Orders { get; }

    public EfUnitOfWork(AppDbContext db)
    {
        _db = db;
        Orders = new SqlOrderRepository(db);
    }

    public Task<int> SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
    public void Dispose() => _db.Dispose();
}
```

## ADO.NET

### Async Patterns
```csharp
public async Task<IReadOnlyList<Customer>> GetCustomersAsync(CancellationToken ct)
{
    var customers = new List<Customer>();
    using (var conn = new SqlConnection(_connectionString))
    using (var cmd = new SqlCommand("SELECT Id, Name FROM Customers", conn))
    {
        await conn.OpenAsync(ct);
        using (var reader = await cmd.ExecuteReaderAsync(ct))
        {
            while (await reader.ReadAsync(ct))
            {
                customers.Add(new Customer(reader.GetInt32(0), reader.GetString(1)));
            }
        }
    }
    return customers.AsReadOnly();
}
```

### Connection Pooling
- Enabled by default. Monitor with performance counters (`.NET CLR Data` → `SqlClient`).
- Set `Min Pool Size` to expected concurrent connections to avoid warm-up latency.
- Always close/dispose connections (returns to pool, not actually closed).

## Dapper (Micro-ORM)
```csharp
// Excellent for performance-critical scenarios
public async Task<Order> GetOrderAsync(int id, CancellationToken ct)
{
    using (var conn = new SqlConnection(_connectionString))
    {
        var order = await conn.QueryFirstOrDefaultAsync<Order>(
            "SELECT * FROM Orders WHERE Id = @id", new { id });

        var items = await conn.QueryAsync<OrderItem>(
            "SELECT * FROM OrderItems WHERE OrderId = @id", new { id });

        order.Items = items.ToList();
        return order;
    }
}
```

## Database Migrations (EF6)
- Use `Enable-Migrations` + `Add-Migration` + `Update-Database`.
- For production: Generate SQL scripts (`Update-Database -Script`) for DBA review.
- Seed data in `Configuration.Seed()` method.
