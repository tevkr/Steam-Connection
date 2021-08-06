using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steam_Connection.Core;
using Steam_Connection.Core.Config;

namespace Steam_Connection.MVVM.ViewModel
{
    class PinCodeViewModel : ObservableObject
    {
        public static RelayCommand ShowBannerCommand { get; set; }
        public static RelayCommand HideBannerCommand { get; set; }
        private bool _bannerVisible;
        private string _errorMessage;

        public bool BannerVisible
        {
            get { return _bannerVisible; }
            set
            {
                _bannerVisible = value;
                OnPropertyChanged(nameof(BannerVisible));
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

        public PinCodeViewModel()
        {
            ErrorMessage = "";
            ShowBannerCommand = new RelayCommand(o =>
            {
                BannerVisible = true;
            });
            HideBannerCommand = new RelayCommand(o =>
            {
                BannerVisible = false;
            });
        }
    }
}
