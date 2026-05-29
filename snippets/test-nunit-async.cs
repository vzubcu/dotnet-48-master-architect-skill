// NUnit Async Test Pattern for .NET 4.8
[TestFixture]
public class OrderServiceTests
{
    private Mock<IOrderRepository> _repoMock;
    private OrderService _service;

    [SetUp]
    public void SetUp()
    {
        _repoMock = new Mock<IOrderRepository>();
        _service = new OrderService(_repoMock.Object, Mock.Of<ILogger>());
    }

    [Test]
    public async Task GetOrderAsync_ExistingId_ReturnsOrder()
    {
        // Arrange
        var expected = new Order { Id = 1, CustomerId = "CUST-1" };
        _repoMock.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(expected);

        // Act
        var result = await _service.GetOrderAsync(1, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(1));
    }

    [Test]
    public void GetOrderAsync_NonExistingId_ThrowsNotFound()
    {
        _repoMock.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                 .ReturnsAsync((Order)null);

        Assert.ThrowsAsync<NotFoundException>(async () =>
            await _service.GetOrderAsync(999, CancellationToken.None));
    }

    [TestCase("CUST-1", 5)]
    [TestCase("CUST-2", 3)]
    public async Task GetOrdersByCustomerAsync_ReturnsCorrectCount(string customerId, int expectedCount)
    {
        var orders = Enumerable.Range(1, expectedCount)
            .Select(i => new Order { CustomerId = customerId })
            .ToList();

        _repoMock.Setup(r => r.FindByCustomerAsync(customerId, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(orders);

        var result = await _service.GetOrdersByCustomerAsync(customerId, CancellationToken.None);

        Assert.That(result.Count, Is.EqualTo(expectedCount));
    }
}
