# Testing Guide for .NET Framework 4.8

## Philosophy

### TDD for New Code
Red → Green → Refactor. Every public method gets a test before implementation.

### Characterization Tests for Legacy
**Never refactor without characterization tests.** Document current behavior first. Lock it. Then improve.

## Test Stack

| Layer | Tools |
|---|---|
| Framework | NUnit 3, xUnit 2, MSTest |
| Mocking | Moq 4.x, NSubstitute, FakeItEasy |
| Assertion | FluentAssertions (optional), built-in |
| Runner | Visual Studio Test Explorer, ReSharper, dotnet test (if using SDK-style) |
| Coverage | dotCover, Visual Studio Enterprise, OpenCover |

## Unit Testing Patterns

### MVP Presenter Test
See `snippets/test-moq-mvp.cs`

### ViewModel Test
See `snippets/test-nunit-async.cs`

### Service with Result<T>
```csharp
[Test]
public void PlaceOrder_EmptyItems_ReturnsFailure()
{
    var result = _service.PlaceOrder(new CreateOrderRequest { Items = new List<OrderItem>() });
    Assert.That(result.IsSuccess, Is.False);
    Assert.That(result.Error, Contains.Substring("at least one item"));
}
```

## Integration Testing

### Database (LocalDB + Transaction Rollback)
See `snippets/test-integration-ef6.cs`

### WCF (Self-Host)
See Test Agent documentation for WCF self-host pattern.

### File System
```csharp
[Test]
public void Export_CreatesFile()
{
    var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
    try
    {
        _exporter.Export(tempPath, data);
        Assert.That(File.Exists(tempPath), Is.True);
        Assert.That(new FileInfo(tempPath).Length, Is.GreaterThan(0));
    }
    finally
    {
        if (File.Exists(tempPath)) File.Delete(tempPath);
    }
}
```

## Coverage Targets

| Code Type | Line | Branch |
|---|---|---|
| New code | >80% | >70% |
| Legacy (post-refactor) | >60% | >50% |
| Critical paths (payments, auth) | 100% | 100% |
| Simple DTOs | >50% | Optional |

## Anti-Patterns to Avoid

| Anti-Pattern | Fix |
|---|---|
| Tests depend on real database | Use LocalDB + rollback or in-memory fake |
| Tests call external APIs | Mock HTTP client or use WireMock |
| Shared mutable state between tests | Fresh fixture per test |
| Assert on exception messages | Assert on exception TYPE |
| Thread.Sleep in tests | Use deterministic sync (ManualResetEventSlim) |
| One test per class | Split by behavior/scenario |
