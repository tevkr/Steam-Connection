using Steam_Connection.Core.Config;
using Steam_Connection.MVVM.Model;
using Steam_Connection.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Steam_Connection.Themes.CustomMessageBox;

namespace Steam_Connection.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для AccountsView.xaml
    /// </summary>
    public partial class AccountsView : UserControl
    {
        public AccountsView()
        {
            InitializeComponent();
        }
        private List<(Point, Point)> generatePoints(int count)
        {
            List<(Point, Point)> points = new List<(Point, Point)>();
            for (int i = 0; i < count; i++)
            {
                if (i % 2 == 0)
                {
                    if (i == 0)
                    {
                        points.Add((new Point(0, 310), new Point(0, 130)));
                    }
                    else
                    {
                        points.Add((new Point(0, 310), new Point(points[i - 2].Item2.Y, points[i - 2].Item2.Y + 140)));
                    }
                }
                else
                {
                    points.Add((new Point(310, 600), new Point(points[i - 1].Item2.X, points[i - 1].Item2.Y)));
                }
            }
            return points;
        }
        private AccountBannerView _draggedItemView;
        private AdornerLayer _adornerLayer;
        private DragAdorner _dragAdorner;
        private List<(Point, Point)> _accountBannersCords;
        private Point _delta;
        async void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem && _draggedItemView == null)
            {
                _draggedItemView = ((ListBoxItem)(sender)).DataContext as AccountBannerView;
                if (_draggedItemView.DADButton.IsMouseOver && _draggedItemView.DADButton.IsEnabled)
                {
                    ListBoxItem draggedItem = sender as ListBoxItem;
                    _delta = new Point(Mouse.GetPosition(draggedItem).X - 150, Mouse.GetPosition(draggedItem).Y - 60);
                    _adornerLayer = AdornerLayer.GetAdornerLayer(listBoxOfAccounts);
                    _dragAdorner = new DragAdorner(listBoxOfAccounts, _draggedItemView, 0.5, e.GetPosition(draggedItem));
                    _adornerLayer.Add(_dragAdorner);
                    _accountBannersCords = generatePoints(Config.getInstance().accounts.Count);
                    //DragDrop.DoDragDrop(_draggedItem, _draggedItem.DataContext, DragDropEffects.Move);
                    //adLayer.Remove(_dragAdorner);
                    //_draggedItem.IsSelected = true;
                }
                else
                {
                    _draggedItemView = null;
                    Config config = Config.getInstance();
                    if (config.steamDir != null && config.steamDir != "")
                    {
                        if (!AccountsViewModel.EditMode)
                        {
                            ListBoxItem lbi = (ListBoxItem)sender;
                            AccountBannerView abv = (AccountBannerView)lbi.Content;
                            AccountsBannerViewModel abvm = (AccountsBannerViewModel)abv.DataContext;
                            if (config.nonConfirmationMode)
                            {
                                if (config.closeMode)
                                {
                                    Application.Current.MainWindow.Hide();
                                    Connector.Connector.connectToSteam(config.accounts[abvm.Id - 1]);
                                    Application.Current.Shutdown();
                                }
                                else
                                {
                                    var task = Task.Factory.StartNew(() =>
                                    {
                                        Connector.Connector.connectToSteamAsync(config.accounts[abvm.Id - 1]);
                                    });
                                    await task;
                                }
                            }
                            else
                            {
                                ((AccountsViewModel)this.DataContext).AccountId = abvm.Id - 1;
                                ((AccountsViewModel)this.DataContext).AccountName = abvm.SteamNickName;
                                ((AccountsViewModel)this.DataContext).NonConfirmationModeBanner = true;
                            }
                        }
                    }
                    else
                    {
                        CustomMessageBox.show((string)Application.Current.FindResource("mb_empty_steam_directory"));
                    }
                }
            }
        }
        private void ListBoxOfAccounts_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragAdorner != null)
            {
                _dragAdorner.PointOffset = e.GetPosition(listBoxOfAccounts);
                foreach (var accountBannerView in AccountsViewModel.AccountBannerViews)
                {
                    accountBannerView.TopLevelBorder.BorderBrush = null;
                }
                Point draggedBennerCenterPoint = new Point(e.GetPosition(listBoxOfAccounts).X - _delta.X,
                    e.GetPosition(listBoxOfAccounts).Y - _delta.Y);
                for (int i = 0; i < _accountBannersCords.Count; i++)
                {
                    if (draggedBennerCenterPoint.X > _accountBannersCords[i].Item1.X &&
                        draggedBennerCenterPoint.X <= _accountBannersCords[i].Item1.Y &&
                        draggedBennerCenterPoint.Y > _accountBannersCords[i].Item2.X &&
                        draggedBennerCenterPoint.Y <= _accountBannersCords[i].Item2.Y)
                    {
                        AccountsViewModel.AccountBannerViews[i].TopLevelBorder.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#4481EB"));;
                    }
                }
            }
        }


        private void ListBoxOfAccounts_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_draggedItemView != null && _dragAdorner != null)
            {
                Config config = Config.getInstance();
                AccountBannerView droppedData = _draggedItemView;
                AccountBannerView target = null;

                for (int i = 0; i < AccountsViewModel.AccountBannerViews.Count; i++)
                {
                    if (AccountsViewModel.AccountBannerViews[i].TopLevelBorder.BorderBrush != null)
                    {
                        target = AccountsViewModel.AccountBannerViews[i];
                        break;
                    }
                }

                _adornerLayer.Remove(_dragAdorner);
                _draggedItemView = null;
                _dragAdorner = null;
                foreach (var accountBannerView in AccountsViewModel.AccountBannerViews)
                {
                    accountBannerView.TopLevelBorder.BorderBrush = null;
                }

                if (target != null)
                {
                    int removedIdx = listBoxOfAccounts.Items.IndexOf(droppedData);
                    int targetIdx = listBoxOfAccounts.Items.IndexOf(target);

                    Account account1 = config.accounts[removedIdx];
                    if (removedIdx < targetIdx)
                    {
                        config.accounts.Insert(targetIdx + 1, account1);
                        config.accounts.RemoveAt(removedIdx);
                    }
                    else
                    {
                        int remIdx = removedIdx + 1;
                        if (config.accounts.Count + 1 > remIdx)
                        {
                            config.accounts.Insert(targetIdx, account1);
                            config.accounts.RemoveAt(remIdx);
                        }
                    }
                    config.saveChanges();
                    AccountsViewModel.fillAccountBannerViews();
                }
            }
        }

    }
}
