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
using System.Windows.Shapes;
using Steam_Connection.Core.Config;
using Steam_Connection.MVVM.View;
using Steam_Connection.MVVM.ViewModel;

namespace Steam_Connection
{
    /// <summary>
    /// Interaction logic for PinCodeWindow.xaml
    /// </summary>
    public partial class PinCodeWindow : Window
    {
        private bool _fisrtLoad;
        public PinCodeWindow()
        {
            InitializeComponent();
            _fisrtLoad = true;
            p1TextBox.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#4481EB"));
            p1TextBox.Focus();
            p1TextBox.Select(0, 0);
        }
        private void exitButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void minimizeButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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
                e.Handled = true;
                if (pIndex == 0) tbs[pIndex].Text = "";
                else
                {
                    tbs[pIndex].Text = "";
                    tbs[pIndex - 1].Focus();
                    tbs[pIndex - 1].Select(0, 1);
                    //tbs[pIndex - 1].SelectionLength = 0;
                }
            }
            else
            {
                e.Handled = true;
                char ch = SettingsView.KeyEventUtility.GetCharFromKey(e.Key);
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
                        tbs[pIndex + 1].Select(0, 0);
                        //tbs[pIndex + 1].SelectionLength = 0;
                    }
                }
            }
            string pinCode = "";
            foreach (var textBox in tbs)
            {
                pinCode += textBox.Text;
            }
            if (pinCode.Length == 4)
            {
                if (isPinCodeCorrect())
                {
                    var location = Window.PointToScreen(new Point(0, 0));
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Title = "Steam Connection";
                    mainWindow.Left = location.X;
                    mainWindow.Top = location.Y;
                    mainWindow.Show();
                    Window.Hide();
                }
                else
                {
                    foreach (var textBox in tbs)
                    {
                        textBox.Text = "";
                    }
                    p1TextBox.Focus();
                    ((PinCodeViewModel) this.DataContext).ErrorMessage = (string)Application.Current.FindResource("error_invalid_pin");
                }
            }
        }

        private bool isPinCodeCorrect()
        {
            string pinCode = p1TextBox.Text + p2TextBox.Text + p3TextBox.Text + p4TextBox.Text;
            Config config = Config.getInstance();
            return config.pinCode == pinCode;
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
            public static extern uint MapVirtualKey(uint uCode, SettingsView.KeyEventUtility.MapType uMapType);

            public static char GetCharFromKey(Key key)
            {
                char ch = ' ';

                int virtualKey = KeyInterop.VirtualKeyFromKey(key);
                var keyboardState = new byte[256];
                GetKeyboardState(keyboardState);

                uint scanCode = MapVirtualKey((uint)virtualKey, SettingsView.KeyEventUtility.MapType.MAPVK_VK_TO_VSC);
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

        private void pinCodeTextBoxes_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_fisrtLoad)
            {
                p1TextBox.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#19000000"));
                _fisrtLoad = false;
            }
        }
        private void ForgotPin_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_fisrtLoad)
            {
                p1TextBox.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#19000000"));
                _fisrtLoad = false;
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Config config = Config.getInstance();
            config.clear();
            var location = Window.PointToScreen(new Point(0, 0));
            MainWindow mainWindow = new MainWindow();
            mainWindow.Left = location.X;
            mainWindow.Top = location.Y;
            mainWindow.Show();
            Window.Hide();
        }


    }
}
