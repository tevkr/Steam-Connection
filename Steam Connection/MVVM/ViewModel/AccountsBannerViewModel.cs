using Steam_Connection.Core;
using Steam_Connection.Core.Config;
using Steam_Connection.MVVM.Model;
using System.Linq;
using System.Net;
using System.Windows;
using Steam_Connection.Themes.CustomMessageBox;

namespace Steam_Connection.MVVM.ViewModel
{
    class AccountsBannerViewModel : ObservableObject
    {
        public RelayCommand DeleteAccoundCommand { get; set; }
        public RelayCommand EditAccountCommand { get; set; }
        private string _steamPicture;
        private string _steamNickname;
        private int _vacCount;
        private string _cSRank;
        private string _d2Rank;
        private int _id;
        private bool _editMode;
        private bool _selected;
        public string SteamPicture
        {
            get { return _steamPicture; }
            set
            {
                _steamPicture = value;
                OnPropertyChanged(nameof(SteamPicture));
            }
        }
        public string SteamNickName
        {
            get { return _steamNickname; }
            set
            {
                _steamNickname = value;
                OnPropertyChanged(nameof(SteamNickName));
            }
        }
        public int VacCount
        {
            get { return _vacCount; }
            set
            {
                _vacCount = value;
                OnPropertyChanged(nameof(VacCount));
            }
        }
        public string cSRank
        {
            get { return _cSRank; }
            set
            {
                _cSRank = value;
                OnPropertyChanged(nameof(cSRank));
            }
        }
        public string d2Rank
        {
            get { return _d2Rank; }
            set
            {
                _d2Rank = value;
                OnPropertyChanged(nameof(d2Rank));
            }
        }
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public bool EditMode
        {
            get { return _editMode; }
            set
            {
                _editMode = value;
                OnPropertyChanged(nameof(EditMode));
            }
        }

        public bool VacMode
        {
            get
            {
                Config config = Config.getInstance();
                return config.vacMode;
            }
        }
        public bool Dota2RanksMode
        {
            get
            {
                Config config = Config.getInstance();
                return config.d2RanksMode;
            }
        }
        public bool CSGORanksMode
        {
            get
            {
                Config config = Config.getInstance();
                return config.cSRanksMode;
            }
        }
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }
        public AccountsBannerViewModel(int id, bool editMode)
        {
            Config config = Config.getInstance();
            Account account = config.accounts.ElementAt(id);
            Id = id + 1;
            SteamPicture = account.steamPicture;
            SteamNickName = account.nickname;
            VacCount = account.vacCount;
            //d2Rank = account.d2Rank.getRank();
            cSRank = account.cSRank.getRank();
            d2Rank = account.d2Rank.getRank();
            EditMode = editMode;
            Selected = false;
            DeleteAccoundCommand = new RelayCommand(o =>
            {
                //MessageBox.Show("123");
                config.accounts.RemoveAt(id);
                config.saveChanges();
                AccountsViewModel.fillAccountBannerViews();
            });
            EditAccountCommand = new RelayCommand(o =>
            {
                if (CheckForInternetConnection())
                {
                    MainViewModel.EditAccountViewCommand.Execute(id);
                }
                else
                {
                    CustomMessageBox.show((string)Application.Current.FindResource("mb_no_internet_connection"));
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
