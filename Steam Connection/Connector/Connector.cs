using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steam_Connection.Core.Config;
using Steam_Connection.MVVM.Model;

namespace Steam_Connection.Connector
{
    class Connector
    {
        public static async void connectToSteamAsync(Account account)
        {
            var task = Task.Factory.StartNew(() =>
            {
                Config config = Config.getInstance();
                Process Taskkill = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "taskkill",
                        Arguments = "/F /IM GameOverlayUI.exe",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                Taskkill.Start();
                Taskkill = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "taskkill",
                        Arguments = "/F /IM Steam.exe",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                Taskkill.Start();
                System.Threading.Thread.Sleep(2000);
                string DataL = "-login" + " " + account.login + " " + account.password;
                Process Steam = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = config.steamDir,
                        Arguments = DataL + " -tcp",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                Steam.Start();
            });
            await task;
        }
        public static void connectToSteam(Account account)
        {
            Config config = Config.getInstance();
            Process Taskkill = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "taskkill",
                    Arguments = "/F /IM GameOverlayUI.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            Taskkill.Start();
            Taskkill = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "taskkill",
                    Arguments = "/F /IM Steam.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            Taskkill.Start();
            System.Threading.Thread.Sleep(2000);
            string DataL = "-login" + " " + account.login + " " + account.password;
            Process Steam = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = config.steamDir,
                    Arguments = DataL + " -tcp",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            Steam.Start();
        }
    }
}
