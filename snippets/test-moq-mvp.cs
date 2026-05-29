// Moq + MVP Testing for WinForms (.NET 4.8)
[TestFixture]
public class CustomerPresenterTests
{
    private Mock<ICustomerView> _viewMock;
    private Mock<ICustomerRepository> _repoMock;
    private CustomerPresenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _viewMock = new Mock<ICustomerView>();
        _repoMock = new Mock<ICustomerRepository>();
        _presenter = new CustomerPresenter(_viewMock.Object, _repoMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        // Unsubscribe to prevent memory leaks in tests
        _presenter?.Dispose();
    }

    [Test]
    public void SaveClicked_WithValidData_CallsRepositorySave()
    {
        _viewMock.Setup(v => v.CustomerName).Returns("John Doe");
        _viewMock.Setup(v => v.Email).Returns("john@example.com");

        _viewMock.Raise(v => v.SaveClicked += null, EventArgs.Empty);

        _repoMock.Verify(r => r.SaveAsync(
            It.Is<Customer>(c => c.Name == "John Doe" && c.Email == "john@example.com"),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public void SaveClicked_WithEmptyName_ShowsErrorAndDoesNotSave()
    {
        _viewMock.Setup(v => v.CustomerName).Returns("");
        _viewMock.Setup(v => v.Email).Returns("john@example.com");

        _viewMock.Raise(v => v.SaveClicked += null, EventArgs.Empty);

        _viewMock.Verify(v => v.ShowMessage(It.Is<string>(s => s.Contains("Name"))), Times.Once);
        _repoMock.Verify(r => r.SaveAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public void SaveClicked_WhenRepositoryThrows_ShowsErrorMessage()
    {
        _viewMock.Setup(v => v.CustomerName).Returns("John");
        _viewMock.Setup(v => v.Email).Returns("john@example.com");
        _repoMock.Setup(r => r.SaveAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()))
                 .ThrowsAsync(new InvalidOperationException("DB error"));

        _viewMock.Raise(v => v.SaveClicked += null, EventArgs.Empty);

        _viewMock.Verify(v => v.ShowMessage(It.Is<string>(s => s.Contains("DB error"))), Times.Once);
    }
}
