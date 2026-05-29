// Async Composition Patterns for .NET 4.8

// Parallel independent operations
public async Task<DashboardData> LoadDashboardAsync(CancellationToken ct)
{
    var ordersTask = _orderService.GetRecentAsync(ct);
    var metricsTask = _metricsService.GetCurrentAsync(ct);
    var alertsTask = _alertService.GetActiveAsync(ct);

    await Task.WhenAll(ordersTask, metricsTask, alertsTask);

    return new DashboardData(
        Orders: await ordersTask,
        Metrics: await metricsTask,
        Alerts: await alertsTask);
}

// Sequential with error handling
public async Task<OrderDetails> GetOrderDetailsAsync(Guid orderId, CancellationToken ct)
{
    var order = await _orderRepository.FindByIdAsync(orderId, ct).ConfigureAwait(false);
    if (order == null)
        throw new NotFoundException($"Order {orderId} not found");

    var customer = await _customerService.GetAsync(order.CustomerId, ct).ConfigureAwait(false);
    var items = await _itemRepository.GetByOrderAsync(orderId, ct).ConfigureAwait(false);

    return new OrderDetails(order, customer, items);
}

// Library code: always ConfigureAwait(false)
public async Task<IReadOnlyList<Order>> GetCustomerOrdersAsync(string customerId, CancellationToken ct)
{
    return await _db.Orders
        .Where(o => o.CustomerId == customerId)
        .AsNoTracking()
        .ToListAsync(ct)
        .ConfigureAwait(false);
}
