// WPF MVVM Base Classes for .NET 4.8
public abstract class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public string Error => null;
    public string this[string columnName] => ValidateProperty(columnName);
    protected abstract string ValidateProperty(string propertyName);
}

public class RelayCommand : ICommand
{
    private readonly Func<Task> _asyncExecute;
    private readonly Func<bool> _canExecute;

    public RelayCommand(Func<Task> asyncExecute, Func<bool> canExecute = null)
    {
        _asyncExecute = asyncExecute ?? throw new ArgumentNullException(nameof(asyncExecute));
        _canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

    public async void Execute(object parameter)
    {
        try { await _asyncExecute(); }
        catch (Exception ex) { /* Log */ }
    }
}
