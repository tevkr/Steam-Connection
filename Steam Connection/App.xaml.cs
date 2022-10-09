using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Steam_Connection.Core.Config;
using Steam_Connection.MVVM.ViewModel;
using Steam_Connection.Themes.CustomMessageBox;

namespace Steam_Connection
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        static Mutex mutex = new Mutex(true, "Steam Connection OneAtATime");
        [STAThread]
        protected override void OnStartup(StartupEventArgs e)
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                var workingDirectory = Environment.CurrentDirectory;
                var projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
                var dotenv = Path.Combine(projectDirectory, ".env");
                DotEnv.Load(dotenv);
                Config config = Config.getInstance();
                if (config.pinMode)
                {
                    PinCodeWindow pinCodeWindow = new PinCodeWindow();
                    pinCodeWindow.Title = "Steam Connection PIN Code";
                    pinCodeWindow.Show();
                }
                else
                {
                    MainWindow mainWindow = new MainWindow();
                    MainViewModel.UpdateVisible = CheckForUpdates();
                    mainWindow.Title = "Steam Connection";
                    mainWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    mainWindow.Show();
                }
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
        private static bool CheckForUpdates()
        {
            try
            {
                using (Process updater = new Process())
                {
                    updater.StartInfo.UseShellExecute = false;
                    updater.StartInfo.RedirectStandardOutput = true;
                    updater.StartInfo.CreateNoWindow = true;
                    updater.StartInfo.FileName = $"{Directory.GetCurrentDirectory()}\\external\\Updater\\Updater.exe";
                    updater.StartInfo.Arguments = $"check V{GetAssemblyFileVersion()}";
                    updater.Start();
                    string response = updater.StandardOutput.ReadToEnd();
                    updater.WaitForExit();
                    return bool.Parse(response);
                }
            }
            catch
            {
                return false;
            }
        }
        private static string GetAssemblyFileVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersion = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersion.FileVersion;
        }
    }
}
