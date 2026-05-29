---
name: "dotnet-48-desktop-agent"
description: "Deep specialist for .NET Framework 4.8 Desktop development. Activate for: WPF, WinForms, XAML, MVVM, MVP, Dispatcher, Control.Invoke, GDI+, memory leaks, COM Interop, cross-thread UI updates, or desktop deployment (ClickOnce, WiX, MSI)."
---

# .NET 4.8 Desktop Specialist

## WPF (Windows Presentation Foundation)

### MVVM Strict Rules
- **Model**: Plain data, no UI logic.
- **View**: XAML only, no code-behind business logic. Code-behind ONLY for view-specific concerns (animations, focus).
- **ViewModel**: `INotifyPropertyChanged`, `ICommand`, `IDataErrorInfo`. NEVER reference View types.

```csharp
public sealed class MainViewModel : INotifyPropertyChanged
{
    private string _statusMessage;
    public string StatusMessage
    {
        get => _statusMessage;
        set { _statusMessage = value; OnPropertyChanged(); }
    }

    public ICommand SaveCommand { get; }

    public MainViewModel(IOrderService service)
    {
        SaveCommand = new RelayCommand(async () => await SaveAsync(service));
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
```

### Memory Leak Prevention
- **Event handlers**: Always unsubscribe. For long-lived objects, use `WeakEventManager`.
- **Bindings**: Avoid binding to non-`INotifyPropertyChanged` objects (causes memory leaks via `PropertyDescriptor`).
- `x:Name` in DataTemplates: Can cause leaks if not managed.

### Cross-Thread UI Updates
```csharp
public void UpdateStatus(string message)
{
    if (!Dispatcher.CheckAccess())
    {
        Dispatcher.Invoke(() => UpdateStatus(message));
        return;
    }
    StatusLabel.Text = message;
}
```

### Performance
- **Virtualization**: `VirtualizingStackPanel` for lists >100 items.
- **UI Virtualization**: `VirtualizingPanel.IsVirtualizing="True"` + `CacheLength`.
- **Deferred Loading**: `TabControl` with `LoadOnDemand` pattern.

## WinForms (Windows Forms)

### MVP Pattern (Mandatory for Testability)
```csharp
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
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try
        {
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
```

### Threading
- **Background → UI**: `Control.Invoke` / `Control.BeginInvoke`. NEVER touch UI from `Task.Run`, `BackgroundWorker.DoWork`, or thread pool.
- **Long operations**: Use `BackgroundWorker` or `async/await` with progress reporting via `IProgress<T>`.

### GDI+ Resource Management
```csharp
// ALWAYS dispose Graphics, Bitmap, Pen, Brush, Font
public void DrawThumbnail(string imagePath, int width, int height)
{
    using (var original = Image.FromFile(imagePath))
    using (var thumbnail = new Bitmap(width, height))
    using (var graphics = Graphics.FromImage(thumbnail))
    {
        graphics.DrawImage(original, 0, 0, width, height);
        thumbnail.Save($"thumb_{imagePath}");
    }
}
```

### COM Interop
- Always release COM objects: `Marshal.ReleaseComObject(comObject)`.
- Use `try-finally` or wrapper classes that implement `IDisposable`.

## Desktop Deployment
- **ClickOnce**: Still supported. SHA384/SHA512 support in 2026 updates.
- **MSI (WiX)**: Enterprise deployments, custom actions with caution.
- **Squirrel**: Auto-updating desktop apps, simpler than ClickOnce.
- **Windows Store**: Desktop Bridge for UWP packaging.
