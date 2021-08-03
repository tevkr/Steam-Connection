using Steam_Connection.Core;
using Steam_Connection.Core.Config;
using Steam_Connection.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Steam_Connection.MVVM.ViewModel
{
    class AccountsViewModel : ObservableObject
    {
        public RelayCommand AddAccountViewOrUpdateCommand { get; set; }
        public RelayCommand EditModeCommand { get; set; }
        public RelayCommand NoButtonCommand { get; set; }
        public RelayCommand YesButtonCommand { get; set; }
        private static bool _editMode;
        private bool _nonConfirmationModeBanner;
        private string _accountName;
        private int _accountId;
        public static bool EditMode
        {
            get { return _editMode; }
            set
            {
                _editMode = value;
                OnStaticPropertyChanged(nameof(EditMode));
            }
        }
        public string AccountName
        {
            get { return _accountName; }
            set
            {
                _accountName = value;
                OnPropertyChanged(nameof(AccountName));
            }
        }
        public int AccountId
        {
            get { return _accountId; }
            set
            {
                _accountId = value;
                OnPropertyChanged(nameof(AccountId));
            }
        }

        private void setSelected(int accountId)
        {
            foreach (var accountBannerView in AccountBannerViews)
            {
                ((AccountsBannerViewModel) accountBannerView.DataContext).Selected = false;
            }
            try
            {
                ((AccountsBannerViewModel)AccountBannerViews[accountId].DataContext).Selected = true;
            }
            catch (Exception e) { }
            
        }

        public bool NonConfirmationModeBanner
        {
            get { return _nonConfirmationModeBanner; }
            set
            {
                _nonConfirmationModeBanner = value;
                if(_nonConfirmationModeBanner) setSelected(AccountId);
                OnPropertyChanged(nameof(NonConfirmationModeBanner));
            }
        }

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
            NonConfirmationModeBanner = false;
            AccountName = "";
            AccountId = -1;
            EditMode = false;
            AddAccountViewOrUpdateCommand = new RelayCommand(o =>
            {
                if ((bool)o)
                    MessageBox.Show("TODO");
                else
                    MainViewModel.AddAccountViewCommand.Execute(null);
            });
            EditModeCommand = new RelayCommand(o =>
            {
                AccountId = -1;
                NonConfirmationModeBanner = false;
                AccountName = "";
                setSelected(AccountId);
                foreach (var AccountBannerView in AccountBannerViews)
                    AccountBannerView.setEditMode((bool)o);
                _editMode = (bool)o;
            });
            NoButtonCommand = new RelayCommand(o =>
            {
                ((AccountsBannerViewModel)AccountBannerViews[AccountId].DataContext).Selected = false;
                AccountId = -1;
                NonConfirmationModeBanner = false;
                AccountName = "";
            });
            YesButtonCommand = new RelayCommand(o =>
            {
                if (config.closeMode) Application.Current.MainWindow.Hide();
                Connector.Connector.connectToSteam(config.accounts[AccountId]);
                if (config.closeMode) { Application.Current.Shutdown(); }
                AccountId = -1;
                NonConfirmationModeBanner = false;
                AccountName = "";
                setSelected(AccountId);
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
