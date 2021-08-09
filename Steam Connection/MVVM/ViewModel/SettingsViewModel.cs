using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Steam_Connection.Core;
using Steam_Connection.Core.Config;
using Steam_Connection.Validations;

namespace Steam_Connection.MVVM.ViewModel
{
    class SettingsViewModel : ObservableObject
    {
        public RelayCommand SaveChangesCommand { get; set; }
        private string _steamDir;
        private bool _langMode;
        private bool _themeMode;
        private bool _vacMode;
        private bool _d2RanksMode;
        private bool _cSRanksMode;
        private bool _closeMode;
        private bool _nonConfirmationMode;
        private bool _pinMode;
        private string _pinDigit1;
        private string _pinDigit2;
        private string _pinDigit3;
        private string _pinDigit4;

        private string _errorMessage;
        public string PinDigit1
        {
            get { return _pinDigit1; }
            set
            {
                _pinDigit1 = value;
                OnPropertyChanged(nameof(PinDigit1));
            }
        }
        public string PinDigit2
        {
            get { return _pinDigit2; }
            set
            {
                _pinDigit2 = value;
                OnPropertyChanged(nameof(PinDigit2));
            }
        }
        public string PinDigit3
        {
            get { return _pinDigit3; }
            set
            {
                _pinDigit3 = value;
                OnPropertyChanged(nameof(PinDigit3));
            }
        }
        public string PinDigit4
        {
            get { return _pinDigit4; }
            set
            {
                _pinDigit4 = value;
                OnPropertyChanged(nameof(PinDigit4));
            }
        }
        public string SteamDirectory
        {
            get { return _steamDir; }
            set
            {
                _steamDir = value;
                OnPropertyChanged(nameof(SteamDirectory));
            }
        }
        public bool LanguageMode
        {
            get { return _langMode; }
            set
            {
                _langMode = value;
                OnPropertyChanged(nameof(LanguageMode));
            }
        }
        public bool ThemeMode
        {
            get { return _themeMode; }
            set
            {
                _themeMode = value;
                OnPropertyChanged(nameof(ThemeMode));
            }
        }
        public bool VacMode
        {
            get { return _vacMode; }
            set
            {
                _vacMode = value;
                OnPropertyChanged(nameof(VacMode));
            }
        }
        public bool Dota2RanksMode
        {
            get { return _d2RanksMode; }
            set
            {
                _d2RanksMode = value;
                OnPropertyChanged(nameof(Dota2RanksMode));
            }
        }
        public bool CSGORanksMode
        {
            get { return _cSRanksMode; }
            set
            {
                _cSRanksMode = value;
                OnPropertyChanged(nameof(CSGORanksMode));
            }
        }
        public bool CloseMode
        {
            get { return _closeMode; }
            set
            {
                _closeMode = value;
                OnPropertyChanged(nameof(CloseMode));
            }
        }
        public bool NonConfirmationMode
        {
            get { return _nonConfirmationMode; }
            set
            {
                _nonConfirmationMode = value;
                OnPropertyChanged(nameof(NonConfirmationMode));
            }
        }
        public bool PinCodeMode
        {
            get { return _pinMode; }
            set
            {
                _pinMode = value;
                OnPropertyChanged(nameof(PinCodeMode));
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
        public SettingsViewModel()
        {
            Config config = Config.getInstance();
            SteamDirectory = config.steamDir;
            if (config.language == config.supportedLanguages[(int)Config.Languages.Russian])
                LanguageMode = false;
            else if (config.language == config.supportedLanguages[(int)Config.Languages.English] || config.language == null)
                LanguageMode = true;
            if (config.theme == Config.Themes.Dark)
                ThemeMode = true;
            else if (config.theme == Config.Themes.Light)
                ThemeMode = false;
            VacMode = config.vacMode;
            Dota2RanksMode = config.d2RanksMode;
            CSGORanksMode = config.cSRanksMode;
            CloseMode = config.closeMode;
            NonConfirmationMode = config.nonConfirmationMode;
            PinCodeMode = config.pinMode;
            if (config.pinMode)
            {
                PinDigit1 = config.pinCode[0].ToString();
                PinDigit2 = config.pinCode[1].ToString();
                PinDigit3 = config.pinCode[2].ToString();
                PinDigit4 = config.pinCode[3].ToString();
            }
            SaveChangesCommand = new RelayCommand(o =>
            {
                ErrorMessage = "";
                if (!SteamDirValidation.isSteamDirCorrect(SteamDirectory))
                {
                    ErrorMessage = (string)Application.Current.FindResource("error_invalid_steam_directory");
                }
                else if (PinCodeMode && 
                         ((PinDigit1 == "" || PinDigit2 == "" || PinDigit3 == "" || PinDigit4 == "") || 
                         (PinDigit1 == null || PinDigit2 == null || PinDigit3 == null || PinDigit4 == null)))
                {
                    ErrorMessage = (string)Application.Current.FindResource("error_empty_pin");
                }
                else if (ErrorMessage == "")
                {
                    config.steamDir = SteamDirectory;
                    config.language = config.supportedLanguages[Convert.ToInt32(LanguageMode)];
                    config.theme = config.supportedThemes[Convert.ToInt32(ThemeMode)];
                    config.vacMode = VacMode;
                    config.d2RanksMode = Dota2RanksMode;
                    config.cSRanksMode = CSGORanksMode;
                    config.closeMode = CloseMode;
                    config.nonConfirmationMode = NonConfirmationMode;
                    config.pinMode = PinCodeMode;
                    config.pinCode = PinDigit1 + PinDigit2 + PinDigit3 + PinDigit4;
                    config.saveChanges();
                }
            });
        }
        
    }
}
