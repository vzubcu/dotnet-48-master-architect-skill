// Characterization Tests for Legacy Code (.NET 4.8)
// RULE: Write these BEFORE touching ANY legacy code

[TestFixture]
public class LegacyOrderProcessorCharacterizationTests
{
    private LegacyOrderProcessor _processor;

    [SetUp]
    public void SetUp()
    {
        _processor = new LegacyOrderProcessor();
    }

    [Test]
    public void CalculateDiscount_VIPCustomer_Returns20Percent()
    {
        // Documents CURRENT behavior — do NOT fix the bug yet
        var result = _processor.CalculateDiscount("VIP", 100m);
        Assert.That(result, Is.EqualTo(20m));
    }

    [Test]
    public void CalculateDiscount_RegularCustomer_Returns5Percent()
    {
        var result = _processor.CalculateDiscount("REGULAR", 100m);
        Assert.That(result, Is.EqualTo(5m));
    }

    [Test]
    public void CalculateDiscount_NullCustomerType_ThrowsNullReference()
    {
        // Documents the crash — this becomes your safety net
        Assert.Throws<NullReferenceException>(() =>
            _processor.CalculateDiscount(null, 100m));
    }

    [Test]
    public void CalculateDiscount_EmptyString_ReturnsZero()
    {
        // Even weird edge cases get documented
        var result = _processor.CalculateDiscount("", 100m);
        Assert.That(result, Is.EqualTo(0m));
    }

    [Test]
    public void CalculateDiscount_NegativeAmount_ReturnsNegativeDiscount()
    {
        // Document the bug before fixing
        var result = _processor.CalculateDiscount("VIP", -100m);
        Assert.That(result, Is.EqualTo(-20m));
    }
}

// After ALL characterization tests pass, you can refactor safely:
// 1. Fix NullReferenceException → return 0 or throw ArgumentException
// 2. Add input validation
// 3. Extract constants (20%, 5%)
// 4. Each refactor step: run tests → green → commit
