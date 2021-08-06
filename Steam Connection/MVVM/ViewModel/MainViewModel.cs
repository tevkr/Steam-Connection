using System;
using Steam_Connection.Core;
using Steam_Connection.MVVM.View;

namespace Steam_Connection.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public static RelayCommand AccountsViewCommand { get; set; }
        public static RelayCommand SettingsViewCommand { get; set; }
        public static RelayCommand AddAccountViewCommand { get; set; }
        public static RelayCommand EditAccountViewCommand { get; set; }
        public AccountsViewModel AccountsVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }
        public AddAccountViewModel AddAccountVM { get; set; }
        public EditAccountView EditAccountV { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public static event EventHandler UpdateAccountsGridVisibleChanged;
        private static bool _updateAccountsGridVisible;
        public static bool UpdateAccountsGridVisible
        {
            get { return _updateAccountsGridVisible; }
            set
            {
                _updateAccountsGridVisible = value;
                UpdateAccountsGridVisibleChanged?.Invoke(null, EventArgs.Empty);
            }
        }
        public static event EventHandler UpdateAccountsProgressChanged;
        private static string _updateAccountsProgress;
        public static string UpdateAccountsProgress
        {
            get { return _updateAccountsProgress; }
            set
            {
                _updateAccountsProgress = value;
                UpdateAccountsProgressChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        public MainViewModel()
        {
            AccountsVM = new AccountsViewModel();
            SettingsVM = new SettingsViewModel();
            AddAccountVM = new AddAccountViewModel();

            CurrentView = AccountsVM;

            AccountsViewCommand = new RelayCommand(o =>
            {
                CurrentView = AccountsVM;
            });

            SettingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = SettingsVM;
            });

            AddAccountViewCommand = new RelayCommand(o =>
            {
                CurrentView = AddAccountVM;
            });

            EditAccountViewCommand = new RelayCommand(o =>
            {
                EditAccountV = new EditAccountView((int)o);
                CurrentView = EditAccountV;
            });
        }
    }
}
