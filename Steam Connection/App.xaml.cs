using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Steam_Connection.Core.Config;

namespace Steam_Connection
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var workingDirectory = Environment.CurrentDirectory;
            var projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            var dotenv = Path.Combine(projectDirectory, ".env");
            DotEnv.Load(dotenv);
            Config config = Config.getInstance();
            if(config.pinMode)
            {
                PinCodeWindow pinCodeWindow = new PinCodeWindow();
                pinCodeWindow.Show();
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
        }
    }
}
