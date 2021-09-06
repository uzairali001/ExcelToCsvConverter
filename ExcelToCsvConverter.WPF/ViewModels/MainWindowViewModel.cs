using System.Windows;
using System.Windows.Input;

using ExcelToCsvConverter.Core;
using ExcelToCsvConverter.Core.Contracts.Services;
using ExcelToCsvConverter.WPF.Helpers;

using Microsoft.Win32;

namespace ExcelToCsvConverter.WPF.ViewModels;

public class MainWindowViewModel : Base.BaseViewModel
{

    #region Properties
    private string? _selectedInputFile;
    public string? SelectedInputFile
    {
        get => _selectedInputFile;
        set => SetValue(ref _selectedInputFile, value);
    }

    private string? _columnSeperator = ",";
    public string? ColumnSeperator
    {
        get => _columnSeperator;
        set => SetValue(ref _columnSeperator, value);
    }

    private string? _optionallyEnclosedBy;
    public string? OptionallyEnclosedBy
    {
        get => _optionallyEnclosedBy;
        set => SetValue(ref _optionallyEnclosedBy, value);
    }

    private string? _selectedOutputLocation;
    public string? SelectedOutputLocation
    {
        get => _selectedOutputLocation;
        set => SetValue(ref _selectedOutputLocation, value);
    }

    private string? _skipRows;
    public string? SkipRows
    {
        get => _skipRows;
        set => SetValue(ref _skipRows, value);
    }

    private bool _overwriteFile = true;
    public bool OverwriteFile
    {
        get => _overwriteFile;
        set => SetValue(ref _overwriteFile, value);
    }
    #endregion

    #region Commands
    private ICommand? _selectInputFileCommand;
    public ICommand SelectInputFileCommand => _selectInputFileCommand ??= new CommandHandler(SelectInputFileAction);

    private ICommand? _selectOutputFileCommand;
    public ICommand SelectOutputFileCommand => _selectOutputFileCommand ??= new CommandHandler(SelectOutputFileAction);


    private ICommand? _proceedCommand;
    public ICommand ProceedCommand => _proceedCommand ??= new CommandHandler(ProceedAction);
    #endregion

    #region Fields
    private readonly IExcelToCsvConverterService _excelToCsvConverter;
    #endregion

    #region Dependencies

    #endregion

    #region Constructors
    public MainWindowViewModel()
    {
        _excelToCsvConverter = Converter.GetConverter();
    }
    #endregion

    #region Public Methods

    #endregion

    #region Actions
    private void SelectInputFileAction()
    {
        OpenFileDialog fileDialog = new()
        {
            Title = "Select input file...",
            Filter = "Excel 2007 (*.xlsx)|*.xlsx|All files (*.*)|*.*",
        };
        if (fileDialog.ShowDialog() is false)
        {
            return;
        }

        SelectedInputFile = fileDialog.FileName;
    }
    private void SelectOutputFileAction()
    {
        SaveFileDialog fileDialog = new()
        {
            Title = "Destination location...",
            Filter = "Comma Seperated File (CSV) (*.csv)|*.csv|Text file (*.txt)|*.txt|All files (*.*)|*.*",
        };
        if (fileDialog.ShowDialog() is false)
        {
            return;
        }

        SelectedOutputLocation = fileDialog.FileName;
    }

    private async void ProceedAction()
    {
        if (SelectedOutputLocation is null || SelectedInputFile is null)
        {
            return;
        }

        if(int.TryParse(SkipRows, out int skipRows) is false)
        {
            skipRows = 0;
        };

        await _excelToCsvConverter.ConvertExcelFileToCSV(SelectedInputFile, new()
        {
            ColumnSeperator = ColumnSeperator ?? ",",
            OptionallyEnclosedBy = OptionallyEnclosedBy,
            OutputFile = SelectedOutputLocation,
            OverwiteOutputFile = OverwriteFile,
            SkipRows = skipRows,
        });

        MessageBox.Show("Done");
    }

    #endregion

    #region Private Methods

    #endregion


}
