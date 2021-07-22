using Steam_Connection.MVVM.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Steam_Connection.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для AccountBannerView.xaml
    /// </summary>
    public partial class AccountBannerView : UserControl
    {
        public AccountBannerView(int id)
        {
            InitializeComponent();
            this.DataContext = new AccountsBannerViewModel(id);
        }
    }
}
