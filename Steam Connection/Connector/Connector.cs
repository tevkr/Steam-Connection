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
        public static void connectToSteam(Account account)
        {
            Config config = Config.getInstance();
            Process.Start("taskkill", "/F /IM GameOverlayUI.exe");
            Process.Start("taskkill", "/F /IM Steam.exe");
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
