using System.Diagnostics;
using ICSharpCode.SharpZipLib.Zip;
using System.Net;

const string LAST_VERSION_URL = "https://raw.githubusercontent.com/tevkr/Steam-Connection/main/last_update.txt";
string GetReleaseVersion()
{
    using (WebClient webClient = new())
    {
        return webClient.DownloadString(LAST_VERSION_URL);
    }
}

bool CheckForUpdates(string currentVersion)
{
    try
    {
        return currentVersion != GetReleaseVersion();
    }
    catch
    {
        return false;
    }
}

bool Update()
{
    try
    {
        string releaseVersion = GetReleaseVersion();
        string zipUrl = $"https://github.com/tevkr/Steam-Connection/releases/download/{releaseVersion}/Steam_Connection_{releaseVersion}.zip";
        using (WebClient webClient = new())
        {
            webClient.DownloadFile(zipUrl, "update.zip");
            Process steamConnection = Process.GetProcessesByName("Steam Connection").FirstOrDefault();
            steamConnection.Kill();
            steamConnection.WaitForExit();
            steamConnection.Dispose();
            FastZip fastZip = new FastZip();
            fastZip.ExtractZip($"update.zip", "../", "-Updater");
            using (Process newSteamConnection = new())
            {
                newSteamConnection.StartInfo.FileName = "Steam Connection.exe";
                newSteamConnection.Start();
                Console.WriteLine("Steam Connection started.");
            }
            File.Delete("update.zip");
        }
        return true;
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return false;
    }
}

// update || check VX.Y.Z.W
string[] commandLineArgs = Environment.GetCommandLineArgs();
if (commandLineArgs.Length == 2 && commandLineArgs[1] == "update")
    Console.Write(Update());
else if (commandLineArgs.Length == 3 && commandLineArgs[1] == "check")
    Console.Write(CheckForUpdates(commandLineArgs[2]));