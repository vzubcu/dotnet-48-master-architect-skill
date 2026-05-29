// Result<T> Pattern — Explicit Success/Failure
public sealed class Result<T>
{
    public bool IsSuccess { get; private set; }
    public T Value { get; private set; }
    public string Error { get; private set; }

    private Result() { }

    public static Result<T> Success(T value) =>
        new Result<T> { IsSuccess = true, Value = value };

    public static Result<T> Failure(string error) =>
        new Result<T> { IsSuccess = false, Error = error };
}

// Usage
public Result<Order> ValidateOrder(CreateOrderRequest request)
{
    if (request == null)
        return Result<Order>.Failure("Request cannot be null");
    if (request.Items == null || request.Items.Count == 0)
        return Result<Order>.Failure("Order must contain at least one item");
    if (request.Amount <= 0)
        return Result<Order>.Failure("Amount must be positive");

    return Result<Order>.Success(new Order(request));
}
