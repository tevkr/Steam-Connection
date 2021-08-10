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
using Steam_Connection.MVVM.Model;

namespace Steam_Connection.MVVM.ViewModel
{
    class AccountsViewModel : ObservableObject
    {
        private static Config config;
        public AsyncRelayCommand AddAccountViewOrUpdateCommand { get; set; }
        public RelayCommand ConnectToSteamCommand { get; set; }
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
        static public event EventHandler AccountBannerViewsChanged;

        private static ObservableCollection<AccountBannerView> _accountBannerViews;
        public static ObservableCollection<AccountBannerView> AccountBannerViews 
        {
            get { return _accountBannerViews; }
            set 
            {
                _accountBannerViews = value;
                AccountBannerViewsChanged?.Invoke(null, EventArgs.Empty);
                //OnStaticPropertyChanged(nameof(AccountBannerViews));
            }
        }

        private string _searchBoxText;

        public string SearchBoxText
        {
            get { return _searchBoxText; }
            set
            {
                _searchBoxText = value;
                fillAccountBannerViews(config.searchByNickname(value), _searchBoxText);
                OnPropertyChanged(nameof(SearchBoxText));
            }
        }


        private async Task addOrUpdate(object o)
        {
            
            if ((bool) o)
            {
                MainViewModel.UpdateAccountsGridVisible = true;
                var task = Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < config.accounts.Count; i++)
                    {
                        MainViewModel.UpdateAccountsProgress = (i + 1) + " / " + config.accounts.Count;
                        config.accounts[i] = new Account(config.accounts[i].steamId64, config.accounts[i].login, config.accounts[i].password);
                    }
                    config.saveChanges();
                });
                await task;
                if (SearchBoxText != null)
                    fillAccountBannerViews(config.searchByNickname(SearchBoxText), SearchBoxText);
                else
                    fillAccountBannerViews();
                MainViewModel.UpdateAccountsGridVisible = false;
                MainViewModel.UpdateAccountsProgress = "";
            }
            else
                MainViewModel.AddAccountViewCommand.Execute(null);
        }

        public AccountsViewModel()
        {
            config = Config.getInstance();
            AccountBannerViews = new ObservableCollection<AccountBannerView>();
            NonConfirmationModeBanner = false;
            AccountName = "";
            AccountId = -1;
            EditMode = false;
            AddAccountViewOrUpdateCommand = new AsyncRelayCommand(async (o) => await addOrUpdate(o));
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
            ConnectToSteamCommand = new RelayCommand(o =>
            { 
                Config config = Config.getInstance();
                MessageBox.Show("123");
                });
            fillAccountBannerViews();
        }
        public static void fillAccountBannerViews(List<int> accountsIndexes = null, string SearchBoxText = null)
        {
            AccountBannerViews.Clear();
            if (accountsIndexes == null)
                accountsIndexes = config.searchByNickname();
            foreach (var index in accountsIndexes)
            {
                AccountBannerViews.Add(new AccountBannerView(index, _editMode));
            }

            if (SearchBoxText != null && SearchBoxText != "")
            {
                foreach (var accountBannerView in AccountBannerViews)
                {
                    accountBannerView.DADButton.Visibility = Visibility.Collapsed;
                }
            }
        }

    }
}
