using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Steam_Connection.Themes.CustomMessageBox
{
    class CustomMessageBox
    {
        public static void show(string text)
        {
            Point currentLocation = new Point(Application.Current.MainWindow.Left,
                Application.Current.MainWindow.Top);
            W_CustomMessageBox customMessageBox = new W_CustomMessageBox();
            customMessageBox.Left = currentLocation.X + 
                                    (Application.Current.MainWindow.Width / 2 - customMessageBox.Width / 2);
            customMessageBox.Top = currentLocation.Y +
                                   (Application.Current.MainWindow.Height / 2 - customMessageBox.Height / 2);
            customMessageBox.TextBlock.Text = text;
            customMessageBox.Topmost = true;
            customMessageBox.Show();
        }
    }
}
