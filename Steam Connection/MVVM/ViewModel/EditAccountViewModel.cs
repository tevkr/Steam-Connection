using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Steam_Connection.Core;
using Steam_Connection.Core.Config;
using Steam_Connection.MVVM.Model;
using Steam_Connection.Validations;

namespace Steam_Connection.MVVM.ViewModel
{
    class EditAccountViewModel : ObservableObject
    {
        public RelayCommand AccountsViewCommand { get; set; }
        public RelayCommand EditAccountCommand { get; set; }
        private string _steamLink;
        private string _steamLogin;
        private string _steamPassword;
        private string _errorMessage;
        public string SteamLink
        {
            get { return _steamLink; }
            set
            {
                _steamLink = value;
                OnPropertyChanged(nameof(SteamLink));
            }
        }
        public string SteamLogin
        {
            get { return _steamLogin; }
            set
            {
                _steamLogin = value;
                OnPropertyChanged(nameof(SteamLogin));
            }
        }
        public string SteamPassword
        {
            get
            {
                return _steamPassword;
            }
            set
            {
                _steamPassword = value;
                OnPropertyChanged(nameof(SteamPassword));
            }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public EditAccountViewModel(int id)
        {
            Config config = Config.getInstance();
            Account currentAccount = config.accounts[id];
            SteamLink = "https://steamcommunity.com/profiles/" + currentAccount.steamId64 + "/";
            SteamLogin = currentAccount.login;
            SteamPassword = currentAccount.password;
            ErrorMessage = "";
            AccountsViewCommand = new RelayCommand(o =>
            {
                MainViewModel.AccountsViewCommand.Execute(null);
            });
            EditAccountCommand = new RelayCommand(o =>
            {
                SteamLinkValidation slv = new SteamLinkValidation(_steamLink);
                if (slv.getSteamLinkType() == SteamLinkValidation.steamLinkTypes.errorType)
                {
                    ErrorMessage = (string)Application.Current.FindResource("error_invalid_steamlink");
                }
                else if (SteamLogin == "" || SteamLogin.Contains(" "))
                {
                    if (SteamLogin == "")
                    {
                        ErrorMessage = (string)Application.Current.FindResource("error_empty_login");
                    }
                    else
                    {
                        ErrorMessage = (string)Application.Current.FindResource("error_invalid_login");
                    }

                }
                else if (SteamPassword == "")
                {
                    ErrorMessage = (string)Application.Current.FindResource("error_empty_password");
                }
                else
                {
                    if (SteamLink != "https://steamcommunity.com/profiles/" + currentAccount.steamId64 + "/")
                    {
                        config.accounts.RemoveAt(id);
                        config.accounts.Insert(id, new Model.Account(slv.getSteamId64(), SteamLogin, SteamPassword));
                    }
                    else
                    {
                        config.accounts[id].login = SteamLogin;
                        config.accounts[id].password = SteamPassword;
                    }
                    config.saveChanges();
                    MainViewModel.AccountsViewCommand.Execute(null);
                }
            });
        }
    }
}