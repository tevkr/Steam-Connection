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
        public RelayCommand EditModeCommand { get; set; }
        private static bool _editMode;

        private static ObservableCollection<AccountBannerView> _accountBannerViews;
        public static ObservableCollection<AccountBannerView> AccountBannerViews 
        {
            get { return _accountBannerViews; }
            set 
            {
                _accountBannerViews = value;
                OnStaticPropertyChanged(nameof(AccountBannerViews));
            }
        }
        public AccountsViewModel()
        {
            Config config = Config.getInstance();
            AccountBannerViews = new ObservableCollection<AccountBannerView>();
            _editMode = false;
            AddAccountViewOrUpdateCommand = new RelayCommand(o =>
            {
                if ((bool)o)
                    MessageBox.Show("TODO");
                else
                    MainViewModel.AddAccountViewCommand.Execute(null);
            });
            EditModeCommand = new RelayCommand(o =>
            {
                foreach (var AccountBannerView in AccountBannerViews)
                    AccountBannerView.setEditMode((bool)o);
                _editMode = (bool)o;
            });
            fillAccountBannerViews();
        }
        public static void fillAccountBannerViews()
        {
            Config config = Config.getInstance();
            AccountBannerViews.Clear();
            for (int i = 0; i < config.accounts.Count; i++)
            {
                AccountBannerViews.Add(new AccountBannerView(i, _editMode));
            }
        }

    }
}
