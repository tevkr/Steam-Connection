using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using Steam_Connection.MVVM.Model;

namespace Steam_Connection.Connector
{
    class Connector
    {
        public static event Action<bool> onConnected;
        public static async void connectToSteamAsync(Account account, bool rememberPassword)
        {
            var task = Task.Factory.StartNew(() =>
            {
                connectToSteam(account, rememberPassword);
            });
            await task;
        }
        private static async void connectToSteam(Account account, bool rememberPassword)
        {
            if (account == null) return;
            Utils.restartSteam();
            if (Utils.getSteamRegistryAutoLoginUser() != account.login)
            {
                Utils.setSteamRegistryAutoLoginUser(string.Empty);
            }
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            Task<bool> loginWindow = Task.Run(() => WaitForSteamLoginWindowAndLoginIn(account, rememberPassword, 30, token));
            Task<bool> mainWindow = Task.Run(() => WaitForSteamMainWindow(token));
            Task<bool> tasksResult = await Task.WhenAny(loginWindow, mainWindow);
            bool connectionResult = await tasksResult;
            cts.Cancel();
            onConnected?.Invoke(connectionResult);
        }
        private static bool WaitForSteamLoginWindowAndLoginIn(Account account, bool rememberPassword, int timeout, CancellationToken token)
        {
            try
            {
                IntPtr steamLoginWindowHandle = Utils.waitForWindow(timeout, Utils.TryGetSteamLoginWindow, token);
                if (steamLoginWindowHandle == IntPtr.Zero) return false;
                using (var automation = new UIA3Automation())
                {
                    AutomationElement window = automation.FromHandle(steamLoginWindowHandle);
                    //window.SetForeground();
                    Utils.ForceWindowToForeground(steamLoginWindowHandle);
                    while (!window.IsAvailable && !window.IsOffscreen)
                    {
                        Thread.Sleep(200);
                    }
                    //window.SetForeground();
                    Utils.ForceWindowToForeground(steamLoginWindowHandle);
                    AutomationElement document = window.FindFirstDescendant(e => e.ByControlType(ControlType.Document));
                    //window.SetForeground();
                    Utils.ForceWindowToForeground(steamLoginWindowHandle);
                    List<TextBox> textBoxes = document.FindAllChildren(e => e.ByControlType(ControlType.Edit)).Select(edit => edit.AsTextBox()).ToList();
                    while (textBoxes.Count != 2)
                    {
                        //window.SetForeground();
                        Utils.ForceWindowToForeground(steamLoginWindowHandle);
                        textBoxes = document.FindAllChildren(e => e.ByControlType(ControlType.Edit)).Select(edit => edit.AsTextBox()).ToList();
                        Thread.Sleep(200);
                    }
                    //window.SetForeground();
                    Utils.ForceWindowToForeground(steamLoginWindowHandle);
                    Button checkBox = document.FindFirstChild(e => e.ByControlType(ControlType.Group)).AsButton();
                    //window.SetForeground();
                    Utils.ForceWindowToForeground(steamLoginWindowHandle);
                    bool rememberPasswordState = document.FindFirstChild(e => e.ByControlType(ControlType.Image)) != null;
                    //window.SetForeground();
                    Utils.ForceWindowToForeground(steamLoginWindowHandle);
                    Button loginButton = document.FindFirstChild(e => e.ByControlType(ControlType.Button)).AsButton();

                    //window.SetForeground();
                    Utils.ForceWindowToForeground(steamLoginWindowHandle);
                    textBoxes[0].Focus();
                    textBoxes[0].WaitUntilEnabled();
                    textBoxes[0].Text = account.login;

                    //window.SetForeground();
                    Utils.ForceWindowToForeground(steamLoginWindowHandle);
                    textBoxes[1].Focus();
                    textBoxes[1].WaitUntilEnabled();
                    textBoxes[1].Text = account.password;

                    if (rememberPassword && !rememberPasswordState)
                    {
                        //window.SetForeground();
                        Utils.ForceWindowToForeground(steamLoginWindowHandle);
                        checkBox.Focus();
                        checkBox.WaitUntilEnabled();
                        checkBox.Invoke();
                    }
                    else if (!rememberPassword && rememberPasswordState)
                    {
                        //window.SetForeground();
                        Utils.ForceWindowToForeground(steamLoginWindowHandle);
                        checkBox.Focus();
                        checkBox.WaitUntilEnabled();
                        checkBox.Invoke();
                    }

                    //window.SetForeground();
                    Utils.ForceWindowToForeground(steamLoginWindowHandle);
                    loginButton.Focus();
                    loginButton.WaitUntilEnabled();
                    loginButton.Invoke();
                }
                return true;
            }
            catch (Exception ex)
            {
                return WaitForSteamLoginWindowAndLoginIn(account, rememberPassword, timeout, token);
            }
        }
        private static bool WaitForSteamMainWindow(CancellationToken token)
        {
            IntPtr steamLoginWindowHandle = Utils.waitForWindow(30, Utils.TryGetSteamMainWindow, token);
            return steamLoginWindowHandle != IntPtr.Zero;
        }
    }
}
