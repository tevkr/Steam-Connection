using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Steam_Connection.Core;
using Steam_Connection.Core.Config;

namespace Steam_Connection.MVVM.ViewModel
{
    class PinCodeViewModel : ObservableObject
    {
        public static RelayCommand ExitCommand { get; set; }
        public static RelayCommand MinimizeCommand { get; set; }
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
        private WindowState _curWindowState;
        public WindowState CurWindowState
        {
            get
            {
                return _curWindowState;
            }
            set
            {
                _curWindowState = value;
                base.OnPropertyChanged("CurWindowState");
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
            ExitCommand = new RelayCommand(o =>
            {
                try
                {
                    Application.Current.Shutdown();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            MinimizeCommand = new RelayCommand(o =>
            {
                try
                {
                    CurWindowState = WindowState.Minimized;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
    }
}
