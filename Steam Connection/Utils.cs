﻿using Microsoft.Win32;
using Steam_Connection.Core.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
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
    }
}
