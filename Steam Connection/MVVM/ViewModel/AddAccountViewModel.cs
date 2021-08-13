using Steam_Connection.Core;
using Steam_Connection.Core.Config;
using Steam_Connection.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Steam_Connection.MVVM.Model;
using Steam_Connection.Parsers;
using Steam_Connection.Themes.CustomMessageBox;

namespace Steam_Connection.MVVM.ViewModel
{
    class AddAccountViewModel : ObservableObject
    {
        public RelayCommand AccountsViewCommand { get; set; }
        public AsyncRelayCommand AddAccountAsyncCommand { get; set; }
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
        private async Task addAccount(object o)
        {
            if (CheckForInternetConnection())
            {
                var task = Task.Factory.StartNew(() =>
                {
                    SteamLinkValidation slv = new SteamLinkValidation(_steamLink);
                    if (slv.getSteamLinkType() == SteamLinkValidation.steamLinkTypes.errorType)
                    {
                        ErrorMessage = (string)Application.Current.FindResource("error_invalid_steamlink"); //"Некорректная ссылка на аккаунт стим.";
                    }
                    else if (SteamLogin == "" || SteamLogin.Contains(" "))
                    {
                        if (SteamLogin == "")
                        {
                            ErrorMessage = (string)Application.Current.FindResource("error_empty_login"); //"Поле с логином пустое.";
                        }
                        else
                        {
                            ErrorMessage = (string)Application.Current.FindResource("error_invalid_login"); //"Некорректное поле с логином.";
                        }

                    }
                    else if (SteamPassword == "")
                    {
                        ErrorMessage = (string)Application.Current.FindResource("error_empty_password"); //"Поле с паролем пустое.";
                    }
                    else
                    {
                        MainViewModel.AddOrEditAccountGridVisible = true;
                        MainViewModel.AddOrEditAccountTitle = (string)Application.Current.FindResource("mw_account_adding");
                        Config config = Config.getInstance();
                        string steamNickname, steamPicture;
                        int vacCount;
                        D2Rank d2Rank;
                        CSRank cSRank;
                        SteamParser steamParser = new SteamParser(slv.getSteamId64());
                        MainViewModel.AddOrEditAccountProgress = (string)Application.Current.FindResource("mw_account_steam_data");
                        steamParser.parseAccInfo();
                        steamParser.parseVacs();
                        steamNickname = steamParser.getNickname();
                        steamPicture = steamParser.getSteamPicture();
                        vacCount = steamParser.getVacCount();
                        Dota2RankParser d2RankParser = new Dota2RankParser(slv.getSteamId64());
                        MainViewModel.AddOrEditAccountProgress = (string)Application.Current.FindResource("mw_account_dota2_data");
                        d2RankParser.parseDota2Rank();
                        d2Rank = d2RankParser.getD2Rank();
                        CSGORankParser cSRankParser = new CSGORankParser(slv.getSteamId64());
                        MainViewModel.AddOrEditAccountProgress = (string)Application.Current.FindResource("mw_account_csgo_data");
                        cSRankParser.parseCSGORank();
                        cSRank = cSRankParser.getCSRank();
                        config.accounts.Add(new Model.Account(slv.getSteamId64(), SteamLogin, SteamPassword,
                            steamNickname, steamPicture, vacCount, d2Rank, cSRank));
                        config.saveChanges();
                        MainViewModel.AccountsViewCommand.Execute(null);
                        MainViewModel.AddOrEditAccountGridVisible = false;
                        MainViewModel.AddOrEditAccountTitle = "";
                        MainViewModel.AddOrEditAccountProgress = "";
                    }
                });
                await task;
            }
            else
            {
                CustomMessageBox.show((string)Application.Current.FindResource("mb_no_internet_connection"));
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
            AddAccountAsyncCommand = new AsyncRelayCommand(async (o) => await addAccount(o));
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
