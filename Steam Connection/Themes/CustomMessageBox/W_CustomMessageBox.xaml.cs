using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Steam_Connection.Themes.CustomMessageBox
{
    /// <summary>
    /// Interaction logic for W_CustomMessageBox.xaml
    /// </summary>
    public partial class W_CustomMessageBox : Window
    {
        public W_CustomMessageBox()
        {
            InitializeComponent();
        }

        private void CloseButton_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void BannerTopPanelGrid_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
