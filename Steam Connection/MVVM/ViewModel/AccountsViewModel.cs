using Steam_Connection.Core;
using Steam_Connection.Core.Config;
using Steam_Connection.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Steam_Connection.MVVM.ViewModel
{
    class AccountsViewModel : ObservableObject
    {
        public RelayCommand AddAccountViewOrUpdateCommand { get; set; }
        public ObservableCollection<AccountBannerView> AccountBannerViews { get; set; }
        public AccountsViewModel()
        {
            Config config = Config.getInstance();
            AccountBannerViews = new ObservableCollection<AccountBannerView>();
            AddAccountViewOrUpdateCommand = new RelayCommand(o =>
            {
                if ((bool)o)
                    MessageBox.Show("TODO");
                else
                    MainViewModel.AddAccountViewCommand.Execute(null);
            });
            for (int i = 0; i < config.accounts.Count; i++)
            {
                AccountBannerViews.Add(new AccountBannerView(i));
            }
        }
    }
}
