using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

using ExcelToCsvConverter.WPF.Pages;

namespace ExcelToCsvConverter.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        protected override async void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length > 0)
            {
                // Command-line arguments were supplied, so run in console mode.
                try
                {
                    _ = SetConsoleTitle("Excel To CSV Converter ConsoleApp");
                    //const int ATTACH_PARENT_PROCESS = -1;
                    //if (AttachConsole(ATTACH_PARENT_PROCESS))
                    //{
                    //    await ConsoleApp.Program.Main(e.Args);
                    //}
                    if (AllocConsole())
                    {
                        await ConsoleApp.Program.Main(e.Args);
                    }

                }
                finally
                {
                    _ = FreeConsole();
                    Shutdown();
                }
            }
            else
            {
                // No command line arguments, so run as normal Windows application.
                //MainWindow mainWindow = new();
                //_ = mainWindow.ShowDialog();
                base.OnStartup(e);
            }
        }

        [DllImport("kernel32")]
        private static extern bool AllocConsole();

        [DllImport("kernel32")]
        private static extern bool AttachConsole(int dwProcessId);

        [DllImport("kernel32")]
        private static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleTitle(String lpConsoleTitle);
    }
}
