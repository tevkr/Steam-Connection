using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using Steam_Connection.MVVM.Model;

namespace Steam_Connection.Connector
{
    class Connector
    {
        public static event Action<bool> onConnected;
        public static async void connectToSteamAsync(Account account)
        {
            var task = Task.Factory.StartNew(() =>
            {
                connectToSteam(account);
            });
            await task;
        }
        public static void connectToSteam(Account account)
        {
            Utils.restartSteam(args: "-login" + " " + account.login + " " + account.password + " -tcp");
            onConnected?.Invoke(true);
        }
        public static async void rememberPasswordConnectToSteamAsync(Account account)
        {
            var task = Task.Factory.StartNew(() =>
            {
                rememberPasswordConnectToSteam(account);
            });
            await task;
        }
        public static void rememberPasswordConnectToSteam(Account account)
        {
            Automation.RemoveAllEventHandlers();
            if (Utils.getSteamRegistryAutoLoginUser() != account.login)
            {
                Utils.setSteamRegistryAutoLoginUser(string.Empty);
            }
            Process steamProcess = Utils.restartSteam();
            int steamCount = 0;
            Automation.AddAutomationEventHandler(
            WindowPattern.WindowOpenedEvent,
            AutomationElement.RootElement,
            TreeScope.Children,
            (sender, e) =>
            {
                var element = sender as AutomationElement;
                if (element.Current.Name.Equals("Steam"))
                {
                    if (++steamCount >= 2)
                    {
                        Automation.RemoveAllEventHandlers();
                        onConnected?.Invoke(true);
                    }
                }
                if (element.Current.ClassName.Equals("vguiPopupWindow") && element.Current.Name.Contains("Steam") && element.Current.Name.Length > 5)
                {
                    Thread.Sleep(500);
                    Utils.ForceWindowToForeground((IntPtr)element.Current.NativeWindowHandle);
                    Utils.SetForegroundWindow((IntPtr)element.Current.NativeWindowHandle);
                    Thread.Sleep(100);
                    if (Utils.getSteamRegistryActiveUser() == 0)
                    {
                        if (string.IsNullOrEmpty(Utils.getSteamRegistryAutoLoginUser()))
                        {
                            foreach (char c in account.login)
                            {
                                Utils.SetForegroundWindow((IntPtr)element.Current.NativeWindowHandle);
                                Thread.Sleep(10);
                                Utils.SendCharacter((IntPtr)element.Current.NativeWindowHandle, c);
                            }
                            Thread.Sleep(100);
                            Utils.SendVirtualKey((IntPtr)element.Current.NativeWindowHandle, Utils.VK.TAB);
                            Thread.Sleep(100);
                        }
                        foreach (char c in account.password)
                        {
                            Utils.SetForegroundWindow((IntPtr)element.Current.NativeWindowHandle);
                            Thread.Sleep(10);
                            Utils.SendCharacter((IntPtr)element.Current.NativeWindowHandle, c);
                        }

                        Utils.SetForegroundWindow((IntPtr)element.Current.NativeWindowHandle);
                        Thread.Sleep(100);
                        Utils.SendVirtualKey((IntPtr)element.Current.NativeWindowHandle, Utils.VK.TAB);
                        Thread.Sleep(100);
                        Utils.SendVirtualKey((IntPtr)element.Current.NativeWindowHandle, Utils.VK.SPACE);

                        Utils.SetForegroundWindow((IntPtr)element.Current.NativeWindowHandle);
                        Thread.Sleep(100);
                        Utils.SendVirtualKey((IntPtr)element.Current.NativeWindowHandle, Utils.VK.RETURN);
                        Automation.RemoveAllEventHandlers();
                    }
                    onConnected?.Invoke(true);
                }
            });
        }
    }
}
