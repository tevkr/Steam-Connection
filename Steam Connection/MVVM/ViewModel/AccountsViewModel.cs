﻿using Steam_Connection.Core;
using Steam_Connection.Core.Config;
using Steam_Connection.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Steam_Connection.MVVM.Model;
using Steam_Connection.Themes.CustomMessageBox;
using System.Windows.Threading;

namespace Steam_Connection.MVVM.ViewModel
{
    class AccountsViewModel : ObservableObject
    {
        private static Config config;
        public AsyncRelayCommand AddAccountViewOrUpdateCommand { get; set; }
        public RelayCommand EditModeCommand { get; set; }
        public RelayCommand NoButtonCommand { get; set; }
        public AsyncRelayCommand YesButtonCommand { get; set; }
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
            if ((bool)o)
            {
                MainViewModel.UpdateAccountsGridVisible = true;
                if (CheckForInternetConnection())
                {
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
                }
                else
                {
                    CustomMessageBox.show((string)Application.Current.FindResource("mb_no_internet_connection"));
                }
                MainViewModel.UpdateAccountsGridVisible = false;
                MainViewModel.UpdateAccountsProgress = "";
            }
            else
                MainViewModel.AddAccountViewCommand.Execute(null);
        }
        private async Task yesCommand()
        {
            int accountIdToConnect = AccountId;
            AccountId = -1;
            NonConfirmationModeBanner = false;
            AccountName = "";
            setSelected(AccountId);
            if (config.closeMode)
            {
                void closeApplication(bool connected)
                {
                    if (config.closeMode)
                    {
                        Application.Current.Dispatcher.InvokeShutdown();
                    }
                }
                Connector.Connector.onConnected += closeApplication;
                Application.Current.MainWindow.Hide();
            }
            await Task.Run(() =>
            {
                Connector.Connector.connectToSteamAsync(config.accounts[accountIdToConnect], config.rememberPasswordMode);
            });
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
                    AccountBannerView.setEditMode((bool) o);
                _editMode = (bool) o;
            });
            NoButtonCommand = new RelayCommand(o =>
            {
                ((AccountsBannerViewModel)AccountBannerViews[AccountId].DataContext).Selected = false;
                AccountId = -1;
                NonConfirmationModeBanner = false;
                AccountName = "";
            });
            YesButtonCommand = new AsyncRelayCommand(async (o) => await yesCommand());
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
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
