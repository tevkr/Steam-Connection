using Steam_Connection.Core;
using Steam_Connection.Core.Config;
using Steam_Connection.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Steam_Connection.MVVM.ViewModel
{
    class AddAccountViewModel : ObservableObject
    {
        public RelayCommand AccountsViewCommand { get; set; }
        public RelayCommand AddAccountCommand { get; set; }
        private string _steamLink;
        private string _steamLogin;
        private string _steampassword;
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
                return _steampassword;
            }
            set
            {
                _steampassword = value;
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
        public AddAccountViewModel()
        {
            Config config = Config.getInstance();
            SteamLink = "";
            SteamLogin = "";
            SteamPassword = "";
            ErrorMessage = "";
            AccountsViewCommand = new RelayCommand(o =>
            {
                MainViewModel.AccountsViewCommand.Execute(null);
            });
            AddAccountCommand = new RelayCommand(o =>
            {
                SteamLinkValidation slv = new SteamLinkValidation(_steamLink);
                if (slv.getSteamLinkType() == SteamLinkValidation.steamLinkTypes.errorType)
                {
                    ErrorMessage = "Некорректная ссылка на аккаунт стим.";
                }
                else if (SteamLogin == "" || SteamLogin.Contains(" "))
                {
                    if (SteamLogin == "")
                    {
                        ErrorMessage = "Поле с логином пустое.";
                    }
                    else
                    {
                        ErrorMessage = "Некорректное поле с логином.";
                    }

                }
                else if (SteamPassword == "")
                {
                    ErrorMessage = "Поле с паролем пустое.";
                }
                else
                {
                    slv.convertSteamLinkToSteamId64Link();
                    config.accounts.Add(new Model.Account(slv.getSteamId64Link(), SteamLogin, SteamPassword));
                    config.saveChanges();
                    MainViewModel.AccountsViewCommand.Execute(null);
                }
            });
        }
    }
}
