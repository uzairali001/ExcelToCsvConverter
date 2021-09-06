using System;
using System.Windows.Input;

namespace ExcelToCsvConverter.WPF.Helpers;

public class CommandHandler : ICommand
{
    private readonly Action _action;
    private readonly Func<bool>? _canExecute;

    /// <summary>
    /// Creates instance of the command handler
    /// </summary>
    /// <param name="action">Action to be executed by the command</param>
    public CommandHandler(Action action) : this(action, null) { }

    /// <summary>
    /// Creates instance of the command handler
    /// </summary>
    /// <param name="action">Action to be executed by the command</param>
    /// <param name="canExecute">A bolean property to containing current permissions to execute the command</param>
    public CommandHandler(Action action, Func<bool>? canExecute)
    {
        _action = action;
        _canExecute = canExecute;
    }

    /// <summary>
    /// Wires CanExecuteChanged event 
    /// </summary>
    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    /// <summary>
    /// Forcess checking if execute is allowed
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke() ?? true;
    }

    public void Execute(object? parameter)
    {
        _action();
    }
}

public class CommandHandler<TParameter> : ICommand
{
    private readonly Action<TParameter> _action;
    private readonly Func<bool>? _canExecute;

    /// <summary>
    /// Creates instance of the command handler
    /// </summary>
    /// <param name="action">Action to be executed by the command</param>
    public CommandHandler(Action<TParameter> action) : this(action, null) { }

    /// <summary>
    /// Creates instance of the command handler
    /// </summary>
    /// <param name="action">Action to be executed by the command</param>
    /// <param name="canExecute">A bolean property to containing current permissions to execute the command</param>
    public CommandHandler(Action<TParameter> action, Func<bool>? canExecute)
    {
        _action = action;
        _canExecute = canExecute;
    }

    /// <summary>
    /// Wires CanExecuteChanged event 
    /// </summary>
    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    /// <summary>
    /// Forcess checking if execute is allowed
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke() ?? true;
    }

    public void Execute(object? parameter)
    {
        if (parameter is not TParameter)
        {
            throw new InvalidCastException($"Parameter is not of type:{typeof(TParameter)}");
        }

        _action((TParameter)Convert.ChangeType(parameter, typeof(TParameter)));
    }
}