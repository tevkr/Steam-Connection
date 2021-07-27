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
using Steam_Connection.MVVM.ViewModel;

namespace Steam_Connection.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для EditAccountView.xaml
    /// </summary>
    public partial class EditAccountView : UserControl
    {
        private EditAccountViewModel currentViewModel;
        public EditAccountView(int id)
        {
            InitializeComponent();
            currentViewModel = new EditAccountViewModel(id);
            this.DataContext = currentViewModel;
        }
    }
}
