using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Steam_Connection.Core;
using Steam_Connection.Core.Config;
using Steam_Connection.MVVM.Model;
using Steam_Connection.Parsers;
using Steam_Connection.Themes.CustomMessageBox;
using Steam_Connection.Validations;

namespace Steam_Connection.MVVM.ViewModel
{
    class EditAccountViewModel : ObservableObject
    {
        public RelayCommand AccountsViewCommand { get; set; }
        public RelayCommand EditAccountCommand { get; set; }
        public AsyncRelayCommand EditAccountAsyncCommand { get; set; }
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
        private async Task editAccount(int id, string steamId64)
        {
            Config config = Config.getInstance();
            if (CheckForInternetConnection())
            {
                var task = Task.Factory.StartNew(() =>
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
                        if (SteamLink != "https://steamcommunity.com/profiles/" + steamId64 + "/")
                        {
                            MainViewModel.AddOrEditAccountGridVisible = true;
                            MainViewModel.AddOrEditAccountTitle = (string)Application.Current.FindResource("mw_account_editing");
                            config.accounts.RemoveAt(id);
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
                            config.accounts.Insert(id, new Model.Account(slv.getSteamId64(), SteamLogin, SteamPassword,
                                steamNickname, steamPicture, vacCount, d2Rank, cSRank));
                            MainViewModel.AddOrEditAccountGridVisible = false;
                            MainViewModel.AddOrEditAccountTitle = "";
                            MainViewModel.AddOrEditAccountProgress = "";
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
                await task;
            }
            else
            {
                if (SteamLink == "https://steamcommunity.com/profiles/" + steamId64 + "/")
                {
                    config.accounts[id].login = SteamLogin;
                    config.accounts[id].password = SteamPassword;
                    config.saveChanges();
                    MainViewModel.AccountsViewCommand.Execute(null);
                }
                else
                    CustomMessageBox.show((string)Application.Current.FindResource("mb_no_internet_connection"));
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
            EditAccountAsyncCommand = new AsyncRelayCommand(async (o) => await editAccount(id, currentAccount.steamId64));
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