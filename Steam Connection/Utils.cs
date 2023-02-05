using Microsoft.Win32;
using Steam_Connection.Core.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Steam_Connection
{
    internal class Utils
    {
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("kernel32.dll")]
        static extern uint GetCurrentThreadId();

        [DllImport("user32.dll")]
        static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetFocus(IntPtr hWnd);
        public delegate bool EnumDelegate(IntPtr hWnd, int lParam);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll", EntryPoint = "GetWindowText", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);
        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        private enum WM : uint
        {
            KEYDOWN = 0x0100,
            KEYUP = 0x0101,
            CHAR = 0x0102
        }
        public enum VK : uint
        {
            RETURN = 0x0D,
            TAB = 0x09,
            SPACE = 0x20
        }
        public enum SW : int
        {
            SHOW = 5
        }
        private static readonly List<char> specialCharacters = new List<char> { '{', '}', '(', ')', '[', ']', '+', '^', '%', '~' };
        public static Process restartSteam(string args = "")
        {
            Config config = Config.getInstance();
            Process taskKillProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "taskkill",
                    Arguments = "/F /IM GameOverlayUI.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            taskKillProcess.Start();
            taskKillProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "taskkill",
                    Arguments = "/F /IM Steam.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            taskKillProcess.Start();
            System.Threading.Thread.Sleep(2000);
            Process steamProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = config.steamDir,
                    Arguments = args,
                    UseShellExecute = true
                }
            };
            steamProcess.Start();
            return steamProcess;
        }
        public static string getSteamRegistryAutoLoginUser()
        {
            string autoLoginUser = string.Empty;
            RegistryKey registryKey = Environment.Is64BitOperatingSystem ? 
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64) : 
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
            try
            {
                registryKey = registryKey.OpenSubKey(@"Software\\Valve\\Steam", false);
                autoLoginUser = registryKey.GetValue("AutoLoginUser").ToString();
            }
            catch { throw; }
            return autoLoginUser;
        }
        public static void setSteamRegistryAutoLoginUser(string autoLoginUser)
        {
            RegistryKey registryKey = Environment.Is64BitOperatingSystem ?
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64) :
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
            try
            {
                registryKey = registryKey.OpenSubKey(@"Software\\Valve\\Steam", true);
                registryKey.SetValue("AutoLoginUser", autoLoginUser, RegistryValueKind.String);
            }
            catch { throw; }
        }
        public static int getSteamRegistryPID()
        {
            int pid = 0;
            RegistryKey registryKey = Environment.Is64BitOperatingSystem ?
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64) :
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
            try
            {
                registryKey = registryKey.OpenSubKey(@"Software\\Valve\\Steam\\ActiveProcess", false);
                pid = Convert.ToInt32(registryKey.GetValue("pid"));
            }
            catch { throw; }
            return pid;
        }
        public static int getSteamRegistryActiveUser()
        {
            int activeUser = 0;
            RegistryKey registryKey = Environment.Is64BitOperatingSystem ?
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64) :
                RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
            try
            {
                registryKey = registryKey.OpenSubKey(@"Software\\Valve\\Steam\\ActiveProcess", false);
                activeUser = Convert.ToInt32(registryKey.GetValue("ActiveUser"));
            }
            catch { throw; }
            return activeUser;
        }
        public static void SendCharacter(IntPtr hwnd, char c)
        {
            Config config = Config.getInstance();
            switch(config.inputMethod)
            {
                case Config.InputMethods.SendMessage:
                    SendMessage(hwnd, (int)WM.CHAR, c, IntPtr.Zero);
                    break;
                case Config.InputMethods.PostMessage:
                    PostMessage(hwnd, (int)WM.CHAR, (IntPtr)c, IntPtr.Zero);
                    break;
                case Config.InputMethods.Send:
                    if (isSpecialCharacter(c))
                        System.Windows.Forms.SendKeys.Send("{" + c.ToString() + "}");
                    else
                        System.Windows.Forms.SendKeys.Send(c.ToString());
                    break;
                case Config.InputMethods.SendWait:
                    if (isSpecialCharacter(c))
                        System.Windows.Forms.SendKeys.SendWait("{" + c.ToString() + "}");
                    else
                        System.Windows.Forms.SendKeys.SendWait(c.ToString());
                    break;
            }
        }
        public static void SendVirtualKey(IntPtr hwnd, VK vk)
        {
            Config config = Config.getInstance();
            switch (config.inputMethod)
            {
                case Config.InputMethods.SendMessage:
                    SendMessage(hwnd, (int)WM.KEYDOWN, (int)vk, IntPtr.Zero);
                    SendMessage(hwnd, (int)WM.KEYUP, (int)vk, IntPtr.Zero);
                    break;
                case Config.InputMethods.PostMessage:
                    PostMessage(hwnd, (int)WM.KEYDOWN, (IntPtr)vk, IntPtr.Zero);
                    PostMessage(hwnd, (int)WM.KEYUP, (IntPtr)vk, IntPtr.Zero);
                    break;
                case Config.InputMethods.Send:
                    System.Windows.Forms.SendKeys.Send(vkToString(vk));
                    break;
                case Config.InputMethods.SendWait:
                    System.Windows.Forms.SendKeys.SendWait(vkToString(vk));
                    break;
            }
        }
        private static bool isSpecialCharacter(char c)
        {
            return specialCharacters.Contains(c);
        }
        private static string vkToString(VK vk)
        {
            if (vk == VK.TAB) return "{TAB}";
            else if (vk == VK.RETURN) return "{ENTER}";
            else return " ";
        }
        private static void AttachedThreadInputAction(Action action)
        {
            var foreThread = GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero);
            var appThread = GetCurrentThreadId();
            bool threadsAttached = false;
            try
            {
                threadsAttached =
                    foreThread == appThread ||
                    AttachThreadInput(foreThread, appThread, true);
                if (threadsAttached) action();
                else throw new ThreadStateException("AttachThreadInput failed.");
            }
            finally
            {
                if (threadsAttached)
                    AttachThreadInput(foreThread, appThread, false);
            }
        }
        public static void ForceWindowToForeground(IntPtr hwnd)
        {
            const int SW_SHOW = 5;
            AttachedThreadInputAction(
                () =>
                {
                    BringWindowToTop(hwnd);
                    ShowWindow(hwnd, SW_SHOW);
                });
        }

        public static IntPtr TryGetSteamLoginWindow()
        {
            IntPtr steamLoginWindow = IntPtr.Zero;
            EnumDelegate filter = delegate (IntPtr hWnd, int lParam)
            {
                StringBuilder strbTitle = new StringBuilder(255);
                int nLength = GetWindowText(hWnd, strbTitle, strbTitle.Capacity + 1);
                string title = strbTitle.ToString();

                StringBuilder strbClassname = new StringBuilder(256);
                int nRet = GetClassName(hWnd, strbClassname, strbClassname.Capacity);
                string classname = strbClassname.ToString();

                if (IsWindowVisible(hWnd) &&
                    !string.IsNullOrEmpty(title) &&
                    !string.IsNullOrEmpty(classname) &&
                    classname.Equals("vguiPopupWindow") &&
                    title.Contains("Steam") &&
                    title.Length > 5)
                {
                    steamLoginWindow = hWnd;
                }
                return true;
            };
            EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero);
            return steamLoginWindow;
        }
        public static IntPtr TryGetSteamMainWindow()
        {
            IntPtr steamMainWindow = IntPtr.Zero;
            EnumDelegate filter = delegate (IntPtr hWnd, int lParam)
            {
                StringBuilder strbTitle = new StringBuilder(255);
                int nLength = GetWindowText(hWnd, strbTitle, strbTitle.Capacity + 1);
                string title = strbTitle.ToString();

                StringBuilder strbClassname = new StringBuilder(256);
                int nRet = GetClassName(hWnd, strbClassname, strbClassname.Capacity);
                string classname = strbClassname.ToString();

                if (!string.IsNullOrEmpty(title) &&
                    !string.IsNullOrEmpty(classname) &&
                    classname.Equals("vguiPopupWindow") &&
                    title.Equals("Steam"))
                {
                    steamMainWindow = hWnd;
                }
                return true;
            };
            EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero);
            return steamMainWindow;
        }
        public static IntPtr waitForWindow(int timeout, Func<IntPtr> TryGetWindow, CancellationToken token)
        {
            IntPtr window = TryGetWindow();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (window == IntPtr.Zero && sw.Elapsed < TimeSpan.FromSeconds(timeout) && !token.IsCancellationRequested)
            {
                window = TryGetWindow();
                Thread.Sleep(200);
            }
            return window;
        }

    }
}
