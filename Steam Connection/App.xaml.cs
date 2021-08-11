﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using Steam_Connection.Core.Config;
using Steam_Connection.Themes.CustomMessageBox;

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
            if (config.pinMode)
            {
                PinCodeWindow pinCodeWindow = new PinCodeWindow();
                pinCodeWindow.Title = "Steam Connection PIN Code";
                pinCodeWindow.Show();
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Title = "Steam Connection";
                mainWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;;
                mainWindow.Show();
            }
            if (!CheckForInternetConnection())
            {
                CustomMessageBox.show((string)Application.Current.FindResource("mb_no_internet_connection"));
            }
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
