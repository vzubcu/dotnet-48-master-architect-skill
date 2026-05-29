// EF6 Integration Test with LocalDB + Transaction Rollback
[TestFixture]
public class OrderRepositoryIntegrationTests
{
    private AppDbContext _db;
    private SqlOrderRepository _repository;
    private TransactionScope _transaction;

    [SetUp]
    public void SetUp()
    {
        _db = new AppDbContext(
            "Server=(localdb)\mssqllocaldb;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=True");
        _repository = new SqlOrderRepository(_db);
        _transaction = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);
    }

    [TearDown]
    public void TearDown()
    {
        _transaction.Dispose(); // Rollback everything
        _db.Dispose();
    }

    [Test]
    public async Task AddAsync_OrderWithItems_PersistsCorrectly()
    {
        var order = new Order("CUST-001");
        order.AddItem(new OrderItem("PROD-1", 2, 10.00m));
        order.AddItem(new OrderItem("PROD-2", 1, 25.00m));

        await _repository.AddAsync(order, CancellationToken.None);

        var saved = await _repository.GetByIdAsync(order.Id, CancellationToken.None);
        Assert.That(saved, Is.Not.Null);
        Assert.That(saved.CustomerId, Is.EqualTo("CUST-001"));
        Assert.That(saved.Items, Has.Count.EqualTo(2));
        Assert.That(saved.TotalAmount, Is.EqualTo(45.00m));
    }

    [Test]
    public async Task FindByCustomerAsync_ReturnsOnlyMatchingOrders()
    {
        var order1 = new Order("CUST-A");
        var order2 = new Order("CUST-B");
        var order3 = new Order("CUST-A");

        await _repository.AddAsync(order1, CancellationToken.None);
        await _repository.AddAsync(order2, CancellationToken.None);
        await _repository.AddAsync(order3, CancellationToken.None);

        var results = await _repository.FindByCustomerAsync("CUST-A", CancellationToken.None);

        Assert.That(results, Has.Count.EqualTo(2));
        Assert.That(results.All(o => o.CustomerId == "CUST-A"), Is.True);
    }
}
