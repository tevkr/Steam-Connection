using Steam_Connection.Core.Config;
using Steam_Connection.MVVM.Model;
using Steam_Connection.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AccountsView.xaml
    /// </summary>
    public partial class AccountsView : UserControl
    {
        public AccountsView()
        {
            InitializeComponent();
        }
        void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem)
            {
                AccountBannerView abv = ((ListBoxItem)(sender)).DataContext as AccountBannerView;
                var obj = new DataObject("AccountBannerView", abv);
                if (abv.DADButton.IsMouseOver)
                {
                    ListBoxItem draggedItem = sender as ListBoxItem;
                    DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Move);
                    draggedItem.IsSelected = true;
                }
            }
        }
        void ListBox_Drop(object sender, DragEventArgs e)
        {
            Config config = Config.getInstance();
            AccountBannerView droppedData = e.Data.GetData(typeof(AccountBannerView)) as AccountBannerView;
            AccountBannerView target = ((ListBoxItem)(sender)).DataContext as AccountBannerView;

            int removedIdx = listBoxOfAccounts.Items.IndexOf(droppedData);
            int targetIdx = listBoxOfAccounts.Items.IndexOf(target);

            Account account1 = config.accounts[removedIdx];
            if (removedIdx < targetIdx)
            {
                config.accounts.Insert(targetIdx + 1, account1);
                config.accounts.RemoveAt(removedIdx);
            }
            else
            {
                int remIdx = removedIdx + 1;
                if (config.accounts.Count + 1 > remIdx)
                {
                    config.accounts.Insert(targetIdx, account1);
                    config.accounts.RemoveAt(remIdx);
                }
            }
            config.saveChanges();
            AccountsViewModel.fillAccountBannerViews();
        }

        void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!AccountsViewModel.EditMode)
            {
                ListBoxItem lbi = (ListBoxItem) e.Source;
                AccountBannerView abv = (AccountBannerView) lbi.Content;
                AccountsBannerViewModel abvm = (AccountsBannerViewModel) abv.DataContext;
                MessageBox.Show(abvm.SteamNickName);
            }
        }
    }
}
