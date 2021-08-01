using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Steam_Connection.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private void DirectoryBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Exe Files (.exe)|*.exe";
            if (fileDialog.ShowDialog() == true)
            {
                DirectoryBox.Text = fileDialog.FileName;
            }
        }

        private void pinTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            /*st<TextBox> tbs = new List<TextBox>();
            tbs.Add(p1TextBox);
            tbs.Add(p2TextBox);
            tbs.Add(p3TextBox);
            tbs.Add(p4TextBox);
            int pIndex = 0;
            for (int i = 0; i < tbs.Count; i++)
            {
                if (tbs[i] == sender)
                {
                    pIndex = i;
                    break;
                }
            }
            if (e.Key == Key.Back)
            {
                if (pIndex == 0) tbs[pIndex].Text = "";
                else
                {
                    tbs[pIndex].Text = "";
                    tbs[pIndex - 1].Focus();
                    tbs[pIndex - 1].SelectionLength = 0;
                }
            }
            else
            {
                /*e.Handled = true;
                if (Char.IsDigit(e.KeyChar))
                {
                    if (pIndex == tbs.Count - 1)
                    {
                        tbs[pIndex].Text = e.KeyChar.ToString();
                    }
                    else
                    {
                        tbs[pIndex].Text = e.KeyChar.ToString();
                        tbs[pIndex + 1].Focus();
                        tbs[pIndex + 1].SelectionLength = 0;
                    }
                }
            }*/
        }

        private void pinTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            List<TextBox> tbs = new List<TextBox>();
            tbs.Add(p1TextBox);
            tbs.Add(p2TextBox);
            tbs.Add(p3TextBox);
            tbs.Add(p4TextBox);
            int pIndex = 0;
            for (int i = 0; i < tbs.Count; i++)
            {
                if (tbs[i] == sender)
                {
                    pIndex = i;
                    break;
                }
            }
            if (e.Key == Key.Back)
            {
                if (pIndex == 0) tbs[pIndex].Text = "";
                else
                {
                    tbs[pIndex].Text = "";
                    tbs[pIndex - 1].Focus();
                    tbs[pIndex - 1].SelectionLength = 0;
                }
            }
            else
            {
                e.Handled = true;
                char ch = KeyEventUtility.GetCharFromKey(e.Key);
                if (Char.IsDigit(ch))
                {
                    if (pIndex == tbs.Count - 1)
                    {
                        tbs[pIndex].Text = ch.ToString();
                    }
                    else
                    {
                        tbs[pIndex].Text = ch.ToString();
                        tbs[pIndex + 1].Focus();
                        tbs[pIndex + 1].SelectionLength = 0;
                    }
                }
            }
        }
        public static class KeyEventUtility
        {
            // ReSharper disable InconsistentNaming
            public enum MapType : uint
            {
                MAPVK_VK_TO_VSC = 0x0,
                MAPVK_VSC_TO_VK = 0x1,
                MAPVK_VK_TO_CHAR = 0x2,
                MAPVK_VSC_TO_VK_EX = 0x3,
            }
            // ReSharper restore InconsistentNaming

            [DllImport("user32.dll")]
            public static extern int ToUnicode(
                uint wVirtKey,
                uint wScanCode,
                byte[] lpKeyState,
                [Out, MarshalAs( UnmanagedType.LPWStr, SizeParamIndex = 4 )]
                StringBuilder pwszBuff,
                int cchBuff,
                uint wFlags);

            [DllImport("user32.dll")]
            public static extern bool GetKeyboardState(byte[] lpKeyState);

            [DllImport("user32.dll")]
            public static extern uint MapVirtualKey(uint uCode, MapType uMapType);

            public static char GetCharFromKey(Key key)
            {
                char ch = ' ';

                int virtualKey = KeyInterop.VirtualKeyFromKey(key);
                var keyboardState = new byte[256];
                GetKeyboardState(keyboardState);

                uint scanCode = MapVirtualKey((uint)virtualKey, MapType.MAPVK_VK_TO_VSC);
                var stringBuilder = new StringBuilder(2);

                int result = ToUnicode((uint)virtualKey, scanCode, keyboardState, stringBuilder, stringBuilder.Capacity, 0);
                switch (result)
                {
                    case -1:
                        break;
                    case 0:
                        break;
                    case 1:
                    {
                        ch = stringBuilder[0];
                        break;
                    }
                    default:
                    {
                        ch = stringBuilder[0];
                        break;
                    }
                }
                return ch;
            }
        }
    }
}
