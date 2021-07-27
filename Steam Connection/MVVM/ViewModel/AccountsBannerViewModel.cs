using Steam_Connection.Core;
using Steam_Connection.Core.Config;
using Steam_Connection.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Steam_Connection.MVVM.ViewModel
{
    class AccountsBannerViewModel : ObservableObject
    {
        public RelayCommand DeleteAccoundCommand { get; set; }
        public RelayCommand hz { get; set; }
        public RelayCommand EditAccountCommand { get; set; }
        private string _steamPicture;
        private string _steamNickname;
        private int _vacCount;
        private string _cSRank;
        private string _d2Rank;
        private int _id;
        private bool _editMode;
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
        public AccountsBannerViewModel(int id, bool editMode)
        {
            Config config = Config.getInstance();
            Account account = config.accounts.ElementAt(id);
            Id = id + 1;
            SteamPicture = account.steamPicture;
            SteamNickName = account.nickname;
            VacCount = account.vacCount;
            //d2Rank = account.d2Rank.getRank();
            cSRank = "/Images/Ranks/CSGO/skillgroup_none.png";
            d2Rank = account.d2Rank.getRank();
            EditMode = editMode;
            DeleteAccoundCommand = new RelayCommand(o =>
            {
                //MessageBox.Show("123");
                config.accounts.RemoveAt(id);
                config.saveChanges();
                AccountsViewModel.fillAccountBannerViews();
            });
            hz = new RelayCommand(o =>
            {
                // TODO
            });
            EditAccountCommand = new RelayCommand(o =>
            {
                MainViewModel.EditAccountViewCommand.Execute(id);
            });
        }
    }
}
