using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Steam_Connection.Core;
using Steam_Connection.MVVM.View;

namespace Steam_Connection.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public static RelayCommand ExitCommand { get; set; }
        public static RelayCommand MinimizeCommand { get; set; }
        public static RelayCommand AccountsViewCommand { get; set; }
        public static RelayCommand SettingsViewCommand { get; set; }
        public static RelayCommand AddAccountViewCommand { get; set; }
        public static RelayCommand EditAccountViewCommand { get; set; }
        public static RelayCommand UpdateCommand { get; set; }
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

        public static event EventHandler SpinnerVisibilityChanged;
        private static bool _spinnerVisibility;
        public static bool SpinnerVisibility
        {
            get { return _spinnerVisibility; }
            set
            {
                _spinnerVisibility = value;
                SpinnerVisibilityChanged?.Invoke(null, EventArgs.Empty);
            }
        }


        public static event EventHandler UpdateButtonEnabledChanged;
        private static bool _updateButtonEnabled;
        public static bool UpdateButtonEnabled
        {
            get { return _updateButtonEnabled; }
            set
            {
                _updateButtonEnabled = value;
                UpdateButtonEnabledChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        public static event EventHandler UpdateButtonVisibilityChanged;
        private static bool _updateButtonVisibility;
        public static bool UpdateButtonVisibility
        {
            get { return _updateButtonVisibility; }
            set
            {
                _updateButtonVisibility = value;
                UpdateButtonVisibilityChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        public static void SetUpdateState(bool state)
        {
            SpinnerVisibility = false;
            UpdateButtonEnabled = state;
            UpdateButtonVisibility = state;
        }

        public static event EventHandler AddOrEditAccountGridVisibleChanged;
        private static bool _addOrEditAccountGridVisible;
        public static bool AddOrEditAccountGridVisible
        {
            get { return _addOrEditAccountGridVisible; }
            set
            {
                _addOrEditAccountGridVisible = value;
                AddOrEditAccountGridVisibleChanged?.Invoke(null, EventArgs.Empty);
            }
        }
        public static event EventHandler AddOrEditAccountProgressChanged;
        private static string _addOrEditAccountProgress;
        public static string AddOrEditAccountProgress
        {
            get { return _addOrEditAccountProgress; }
            set
            {
                _addOrEditAccountProgress = value;
                AddOrEditAccountProgressChanged?.Invoke(null, EventArgs.Empty);
            }
        }
        public static event EventHandler AddOrEditAccountTitleChanged;
        private static string _addOrEditAccountTitle;
        public static string AddOrEditAccountTitle
        {
            get { return _addOrEditAccountTitle; }
            set
            {
                _addOrEditAccountTitle = value;
                AddOrEditAccountTitleChanged?.Invoke(null, EventArgs.Empty);
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

        public MainViewModel()
        {
            SpinnerVisibility = true;
            UpdateButtonEnabled = false;
            UpdateButtonVisibility = true;

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

            UpdateCommand = new RelayCommand(o =>
            {
                try
                {
                    using (Process updater = new Process())
                    {
                        updater.StartInfo.UseShellExecute = false;
                        updater.StartInfo.RedirectStandardOutput = true;
                        updater.StartInfo.CreateNoWindow = true;
                        updater.StartInfo.FileName = $"{Directory.GetCurrentDirectory()}\\external\\Updater\\Updater.exe";
                        updater.StartInfo.Arguments = "update";
                        updater.Start();
                    }
                }
                catch (Exception ex)
                {
                    Process.Start("https://github.com/tevkr/Steam-Connection/releases");
                }
            });
        }
    }
}
