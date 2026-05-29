// WinForms MVP Pattern for .NET 4.8
public interface ICustomerView
{
    string CustomerName { get; set; }
    string Email { get; set; }
    event EventHandler SaveClicked;
    event EventHandler CancelClicked;
    void ShowMessage(string message);
    void CloseView();
}

public class CustomerPresenter
{
    private readonly ICustomerView _view;
    private readonly ICustomerRepository _repository;

    public CustomerPresenter(ICustomerView view, ICustomerRepository repository)
    {
        _view = view ?? throw new ArgumentNullException(nameof(view));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _view.SaveClicked += OnSaveClicked;
        _view.CancelClicked += (s, e) => _view.CloseView();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(_view.CustomerName))
            {
                _view.ShowMessage("Name is required");
                return;
            }

            var customer = new Customer(_view.CustomerName, _view.Email);
            await _repository.SaveAsync(customer);
            _view.ShowMessage("Saved successfully");
            _view.CloseView();
        }
        catch (Exception ex)
        {
            _view.ShowMessage($"Error: {ex.Message}");
        }
    }
}

// WinForms implementation
public partial class CustomerForm : Form, ICustomerView
{
    public string CustomerName { get => txtName.Text; set => txtName.Text = value; }
    public string Email { get => txtEmail.Text; set => txtEmail.Text = value; }
    public event EventHandler SaveClicked;
    public event EventHandler CancelClicked;

    public CustomerForm()
    {
        InitializeComponent();
        btnSave.Click += (s, e) => SaveClicked?.Invoke(this, EventArgs.Empty);
        btnCancel.Click += (s, e) => CancelClicked?.Invoke(this, EventArgs.Empty);
    }

    public void ShowMessage(string message) => MessageBox.Show(message);
}
