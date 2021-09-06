using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExcelToCsvConverter.WPF.ViewModels.Base;

public class BaseViewModel : INotifyPropertyChanged
{
    private bool isBusy = false;
    public bool IsBusy
    {
        get => isBusy;
        set => SetValue(ref isBusy, value);
    }

    private string title = string.Empty;
    public string Title
    {
        get => title;
        set => SetValue(ref title, value);
    }

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    protected void SetValue<T>(ref T backingFiled, T value,
        [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingFiled, value)) return;
        backingFiled = value;
        OnPropertyChanged(propertyName);
    }
    #endregion
}
