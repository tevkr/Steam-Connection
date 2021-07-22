using System;
using Steam_Connection.Core;

namespace Steam_Connection.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public static RelayCommand AccountsViewCommand { get; set; }
        public static RelayCommand SettingsViewCommand { get; set; }
        public static RelayCommand AddAccountViewCommand { get; set; }

        public AccountsViewModel AccountsVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }
        public AddAccountViewModel AddAccountVM { get; set; }

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
        }
    }
}
