using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam_Connection
{
    internal class Updater
    {
        public static bool CheckForUpdates()
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
