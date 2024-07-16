using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

using ALEX.Contracts.Services;
using ALEX.Helpers;
using ALEX.Properties;

using Syncfusion.UI.Xaml.NavigationDrawer;
using Syncfusion.Windows.Shared;

namespace ALEX.ViewModels
{
    public class ShellViewModel : Observable
    {
        private ICommand _optionsMenuItemInvokedCommand;

        private readonly INavigationService _navigationService;
        private object _selectedMenuItem;
        private RelayCommand _goBackCommand;
        private ICommand _menuItemInvokedCommand;
        private ICommand _loadedCommand;
        private ICommand _unloadedCommand;

        public object SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set
            {
                if (value as NavigationPaneItem == null)
                {
                    Set(ref _selectedMenuItem, ((FrameworkElement)value).DataContext, "SelectedMenuItem");
                }
                else
                {
                    Set(ref _selectedMenuItem, value, "SelectedMenuItem");
                }
                //NavigateTo((_selectedMenuItem as NavigationPaneItem).TargetType);
				if (_selectedMenuItem is NavigationPaneItem navigationPaneItem && navigationPaneItem.TargetType != null)
                {
                    NavigateTo(navigationPaneItem.TargetType);
                }
            }
        }

        public void UpdateFillColor(SolidColorBrush FillColor)
        {
            foreach (var item in MenuItems)
            {
                (item as NavigationPaneItem).Path.Fill = FillColor;
            }
            SetttingsIconColor = FillColor;
        }

        private SolidColorBrush setttingsIconColor;

        public SolidColorBrush SetttingsIconColor
        {
            get { return setttingsIconColor; }
            set
            {
                setttingsIconColor = value;
                OnPropertyChanged(nameof(SetttingsIconColor));
            }
        }

        // TODO WTS: Change the icons and titles for all HamburgerMenuItems here.
        public ObservableCollection<NavigationPaneItem> MenuItems { get; set; } = new ObservableCollection<NavigationPaneItem>()
        {
        	new NavigationPaneItem() { 
                        Label = Resources.ShellChartsPage,
                        Path = new Path()
                        {
                            Width = 15,
                            Height = 15,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Data = Geometry.Parse("M22 0C19.7909 0 18 1.79086 18 4V44C18 46.2091 19.7909 48 22 48H26C28.2091 48 30 46.2091 30 44V4C30 1.79086 28.2091 0 26 0H22ZM20 4C20 2.89543 20.8954 2 22 2H26C27.1046 2 28 2.89543 28 4V44C28 45.1046 27.1046 46 26 46H22C20.8954 46 20 45.1046 20 44V4ZM4 22C1.79086 22 0 23.7909 0 26V44C0 46.2091 1.79086 48 4 48H8C10.2091 48 12 46.2091 12 44V26C12 23.7909 10.2091 22 8 22H4ZM2 26C2 24.8954 2.89543 24 4 24H8C9.10457 24 10 24.8954 10 26V44C10 45.1046 9.10457 46 8 46H4C2.89543 46 2 45.1046 2 44V26ZM40 15C37.7909 15 36 16.7909 36 19V44C36 46.2091 37.7909 48 40 48H44C46.2091 48 48 46.2091 48 44V19C48 16.7909 46.2091 15 44 15H40ZM38 19C38 17.8954 38.8954 17 40 17H44C45.1046 17 46 17.8954 46 19V44C46 45.1046 45.1046 46 44 46H40C38.8954 46 38 45.1046 38 44V19Z"),
                            Fill = new SolidColorBrush(Colors.Black),
                            Stretch = Stretch.Fill,
                        },
                        TargetType = typeof(ChartsViewModel) 
            },
        	new NavigationPaneItem() { 
                        Label = Resources.ShellKanbanPage,
                        Path = new Path()
                        {
                            Width = 15,
                            Height = 15,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Data = Geometry.Parse("M0 6C0 2.68629 2.68629 0 6 0H42C45.3137 0 48 2.68629 48 6V42C48 45.3137 45.3137 48 42 48H6C2.68629 48 0 45.3137 0 42V6ZM6 2C3.79086 2 2 3.79086 2 6V42C2 44.2091 3.79086 46 6 46H42C44.2091 46 46 44.2091 46 42V6C46 3.79086 44.2091 2 42 2H6ZM5 5C5 4.44772 5.44772 4 6 4H15C15.5523 4 16 4.44772 16 5C16 5.55228 15.5523 6 15 6H6C5.44772 6 5 5.55228 5 5ZM18 5C18 4.44772 18.4477 4 19 4H29C29.5523 4 30 4.44772 30 5C30 5.55228 29.5523 6 29 6H19C18.4477 6 18 5.55228 18 5ZM32 5C32 4.44772 32.4477 4 33 4H42C42.5523 4 43 4.44772 43 5C43 5.55228 42.5523 6 42 6H33C32.4477 6 32 5.55228 32 5ZM5 10.5C5 9.11929 6.11929 8 7.5 8H13.5C14.8807 8 16 9.11929 16 10.5V13.5C16 14.8807 14.8807 16 13.5 16H7.5C6.11929 16 5 14.8807 5 13.5V10.5ZM7.5 10C7.22386 10 7 10.2239 7 10.5V13.5C7 13.7761 7.22386 14 7.5 14H13.5C13.7761 14 14 13.7761 14 13.5V10.5C14 10.2239 13.7761 10 13.5 10H7.5ZM18 10.5C18 9.11929 19.1193 8 20.5 8H27.5C28.8807 8 30 9.11929 30 10.5V16.5C30 17.8807 28.8807 19 27.5 19H20.5C19.1193 19 18 17.8807 18 16.5V10.5ZM20.5 10C20.2239 10 20 10.2239 20 10.5V16.5C20 16.7761 20.2239 17 20.5 17H27.5C27.7761 17 28 16.7761 28 16.5V10.5C28 10.2239 27.7761 10 27.5 10H20.5ZM32 10.5C32 9.11929 33.1193 8 34.5 8H40.5C41.8807 8 43 9.11929 43 10.5V11.5C43 12.8807 41.8807 14 40.5 14H34.5C33.1193 14 32 12.8807 32 11.5V10.5ZM34.5 10C34.2239 10 34 10.2239 34 10.5V11.5C34 11.7761 34.2239 12 34.5 12H40.5C40.7761 12 41 11.7761 41 11.5V10.5C41 10.2239 40.7761 10 40.5 10H34.5ZM32 18.5C32 17.1193 33.1193 16 34.5 16H40.5C41.8807 16 43 17.1193 43 18.5V21.5C43 22.8807 41.8807 24 40.5 24H34.5C33.1193 24 32 22.8807 32 21.5V18.5ZM34.5 18C34.2239 18 34 18.2239 34 18.5V21.5C34 21.7761 34.2239 22 34.5 22H40.5C40.7761 22 41 21.7761 41 21.5V18.5C41 18.2239 40.7761 18 40.5 18H34.5ZM5 20.5C5 19.1193 6.11929 18 7.5 18H13.5C14.8807 18 16 19.1193 16 20.5V25.5C16 26.8807 14.8807 28 13.5 28H7.5C6.11929 28 5 26.8807 5 25.5V20.5ZM7.5 20C7.22386 20 7 20.2239 7 20.5V25.5C7 25.7761 7.22386 26 7.5 26H13.5C13.7761 26 14 25.7761 14 25.5V20.5C14 20.2239 13.7761 20 13.5 20H7.5ZM18 23.5C18 22.1193 19.1193 21 20.5 21H27.5C28.8807 21 30 22.1193 30 23.5V32.5C30 33.8807 28.8807 35 27.5 35H20.5C19.1193 35 18 33.8807 18 32.5V23.5ZM20.5 23C20.2239 23 20 23.2239 20 23.5V32.5C20 32.7761 20.2239 33 20.5 33H27.5C27.7761 33 28 32.7761 28 32.5V23.5C28 23.2239 27.7761 23 27.5 23H20.5ZM32 28.5C32 27.1193 33.1193 26 34.5 26H40.5C41.8807 26 43 27.1193 43 28.5V31.5C43 32.8807 41.8807 34 40.5 34H34.5C33.1193 34 32 32.8807 32 31.5V28.5ZM34.5 28C34.2239 28 34 28.2239 34 28.5V31.5C34 31.7761 34.2239 32 34.5 32H40.5C40.7761 32 41 31.7761 41 31.5V28.5C41 28.2239 40.7761 28 40.5 28H34.5ZM32 38.5C32 37.1193 33.1193 36 34.5 36H40.5C41.8807 36 43 37.1193 43 38.5V41.5C43 42.8807 41.8807 44 40.5 44H34.5C33.1193 44 32 42.8807 32 41.5V38.5ZM34.5 38C34.2239 38 34 38.2239 34 38.5V41.5C34 41.7761 34.2239 42 34.5 42H40.5C40.7761 42 41 41.7761 41 41.5V38.5C41 38.2239 40.7761 38 40.5 38H34.5Z"),
                            Fill = new SolidColorBrush(Colors.Black),
                            Stretch = Stretch.Fill,
                        },
                        TargetType = typeof(KanbanViewModel) 
            },
        	new NavigationPaneItem() { 
                        Label = Resources.ShellMainPage,
                        Path = new Path()
                        {
                            Width = 15,
                            Height = 15,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Data = Geometry.Parse("M28.414 4H7V44H39V14.586ZM29 7.414 35.586 14H29ZM9 42V6H27V16H37V42Z"),
                            Fill = new SolidColorBrush(Colors.Black),
                            Stretch = Stretch.Fill,
                        },
                        TargetType = typeof(MainViewModel) 
            },
        	new NavigationPaneItem() { 
                        Label = Resources.ShellRangeSliderPage,
                        Path = new Path()
                        {
                            Width = 15,
                            Height = 15,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Data = Geometry.Parse("M23 7.27271C23 7.82499 23.4477 8.27271 24 8.27271C24.5523 8.27271 25 7.82499 25 7.27271V2.02274C27.2207 2.12378 29.4172 2.56102 31.5136 3.32283C34.2199 4.30624 36.7068 5.81132 38.8331 7.75261L35.1209 11.4649C34.7303 11.8554 34.7303 12.4886 35.1209 12.8791C35.5114 13.2696 36.1446 13.2696 36.5351 12.8791L40.2474 9.16682C41.7462 10.8085 42.9902 12.6709 43.9339 14.692C45.1522 17.301 45.8464 20.1237 45.9773 23H40.7273C40.175 23 39.7273 23.4477 39.7273 24C39.7273 24.5523 40.175 25 40.7273 25H45.9773C45.8762 27.2207 45.439 29.4172 44.6772 31.5136C43.6905 34.229 42.1786 36.7234 40.2279 38.8545C38.2772 40.9856 35.9258 42.7116 33.308 43.9339C30.6903 45.1562 27.8574 45.851 24.9711 45.9786C22.0849 46.1061 19.2017 45.6639 16.4864 44.6772C13.771 43.6905 11.2766 42.1786 9.14551 40.2279C7.01443 38.2772 5.28845 35.9258 4.0661 33.308C2.84783 30.699 2.15361 27.8763 2.02274 25H7.27273C7.82501 25 8.27273 24.5523 8.27273 24C8.27273 23.4477 7.82501 23 7.27273 23H2.02274C2.12378 20.7793 2.56102 18.5828 3.32283 16.4864C4.30624 13.7801 5.81134 11.2932 7.75265 9.1668L11.465 12.8791C11.8555 13.2696 12.4886 13.2696 12.8792 12.8791C13.2697 12.4886 13.2697 11.8554 12.8792 11.4649L9.16687 7.75259C10.8086 6.25378 12.6709 5.00984 14.692 4.0661C17.301 2.84783 20.1237 2.15361 23 2.02274V7.27271ZM7.03129 7.02758C6.78109 7.27773 6.53621 7.53362 6.29686 7.7951C4.1688 10.1199 2.51949 12.8411 1.44308 15.8033C0.36668 18.7655 -0.115731 21.9108 0.023394 25.0594C0.162519 28.2081 0.920454 31.2985 2.25393 34.1542C3.5874 37.01 5.47029 39.5751 7.7951 41.7031C10.1199 43.8312 12.8411 45.4805 15.8033 46.5569C18.7655 47.6333 21.9108 48.1157 25.0594 47.9766C28.2081 47.8375 31.2985 47.0795 34.1542 45.7461C37.01 44.4126 39.5751 42.5297 41.7031 40.2049C43.8312 37.8801 45.4805 35.1589 46.5569 32.1967C47.5125 29.567 47.9999 26.7931 48 24.0006C48 23.6474 47.9922 23.294 47.9766 22.9406C47.8375 19.7919 47.0795 16.7015 45.7461 13.8458C44.5632 11.3126 42.9481 9.00812 40.9756 7.03452L40.9706 7.0294L40.9654 7.02432C40.7175 6.77654 40.464 6.53399 40.2049 6.29686C37.8801 4.1688 35.1589 2.51949 32.1967 1.44308C29.569 0.488227 26.7972 0.000790708 24.0068 0L24 -2.28882e-05L23.9932 0C23.6425 0.000100126 23.2916 0.00788542 22.9406 0.023394C19.7919 0.162519 16.7015 0.920454 13.8458 2.25393C11.3112 3.43742 9.00561 5.05369 7.03129 7.02758ZM24.8221 29.5047C24.4398 30.1668 23.5931 30.3937 22.931 30.0114C22.2688 29.6291 22.042 28.7824 22.4243 28.1203C22.4238 28.1211 22.424 28.1208 22.425 28.1192C22.4302 28.111 22.4581 28.0669 22.5231 27.9756C22.5936 27.8765 22.6885 27.7489 22.8089 27.5921C23.0497 27.2786 23.3692 26.8789 23.7535 26.409C24.521 25.4706 25.5176 24.2882 26.5981 23.023C26.9664 22.5918 27.3437 22.1518 27.724 21.7097C27.5313 22.2601 27.3389 22.8068 27.1495 23.3414C26.5941 24.9097 26.0684 26.364 25.6395 27.4979C25.4247 28.0657 25.2383 28.5422 25.0872 28.9074C25.0116 29.0902 24.9485 29.2361 24.898 29.3468C24.847 29.4585 24.8227 29.5032 24.8219 29.505C24.8218 29.5052 24.8219 29.505 24.8221 29.5047ZM30.359 15.6221C30.0803 15.941 29.7865 16.2775 29.4817 16.6272C26.0744 20.5356 21.2866 26.0909 20.6922 27.1203C19.7576 28.739 20.3123 30.8089 21.931 31.7434C23.5497 32.678 25.6196 32.1234 26.5541 30.5047C27.1485 29.4752 29.5656 22.5512 31.2467 17.6462C31.3971 17.2074 31.5416 16.7847 31.6784 16.3839C31.8191 15.9716 31.9516 15.5825 32.0739 15.2226L32.1396 15.0294C32.315 14.513 31.661 14.1353 31.3015 14.5455L31.167 14.699C30.9165 14.9849 30.6457 15.2942 30.359 15.6221Z"),
                            Fill = new SolidColorBrush(Colors.Black),
                            Stretch = Stretch.Fill,
                        },
                        TargetType = typeof(RangeSliderViewModel) 
            },
        	new NavigationPaneItem() { 
                        Label = Resources.ShellSchedulerPage,
                        Path = new Path()
                        {
                            Width = 15,
                            Height = 15,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Data = Geometry.Parse("M9 0C9.55229 0 10 0.447715 10 1V4H39V1C39 0.447715 39.4477 0 40 0C40.5523 0 41 0.447715 41 1V4H42C45.3137 4 48 6.68629 48 10V17V20V22.7152C47.5488 21.7827 46.858 20.9719 46 20.3821V20V18H13H2V42C2 44.2091 3.79086 46 6 46H13H27.2941L27.5661 47.5411C27.5947 47.7033 27.6417 47.8568 27.7045 48H13H6C2.68629 48 0 45.3137 0 42V17V10C0 6.68629 2.68629 4 6 4H8V1C8 0.447715 8.44771 0 9 0ZM47.8327 28.1011C47.8922 27.9941 47.9479 27.8865 48 27.7786V42C48 45.3137 45.3137 48 42 48H38H32.8L37.2433 46.1486C37.3459 46.1059 37.4426 46.056 37.5332 46H38H42C44.2091 46 46 44.2091 46 42V31.4L47.8327 28.1011ZM38.5 21C38.5477 21 38.5952 21.001 38.6424 21.0028C38.2663 21.3621 37.9361 21.7731 37.6634 22.2277L37.2 23H35.5C34.6716 23 34 23.6716 34 24.5V26.5C34 26.9219 34.1742 27.3031 34.4546 27.5757L33.4137 29.3105C32.5558 28.6726 32 27.6512 32 26.5V24.5C32 22.567 33.567 21 35.5 21H38.5ZM28.9939 37.2915C28.9148 37.3834 28.8427 37.4839 28.779 37.5931L26.8694 40.8667C26.6309 41.2756 26.5451 41.7558 26.6274 42.222L26.7258 42.7794C26.3443 42.922 25.9313 43 25.5 43H22.5C20.567 43 19 41.433 19 39.5V37.5C19 35.567 20.567 34 22.5 34H25.5C27.363 34 28.8861 35.4556 28.9939 37.2915ZM8 6H6C3.79086 6 2 7.79086 2 10V16H13H46V10C46 7.79086 44.2091 6 42 6H41V9C41 9.55229 40.5523 10 40 10C39.4477 10 39 9.55229 39 9V6H10V9C10 9.55229 9.55229 10 9 10C8.44771 10 8 9.55229 8 9V6ZM9.5 21C7.567 21 6 22.567 6 24.5V26.5C6 28.433 7.567 30 9.5 30H12.5C14.433 30 16 28.433 16 26.5V24.5C16 22.567 14.433 21 12.5 21H9.5ZM8 24.5C8 23.6716 8.67157 23 9.5 23H12.5C13.3284 23 14 23.6716 14 24.5V26.5C14 27.3284 13.3284 28 12.5 28H9.5C8.67157 28 8 27.3284 8 26.5V24.5ZM19 24.5C19 22.567 20.567 21 22.5 21H25.5C27.433 21 29 22.567 29 24.5V26.5C29 28.433 27.433 30 25.5 30H22.5C20.567 30 19 28.433 19 26.5V24.5ZM22.5 23C21.6716 23 21 23.6716 21 24.5V26.5C21 27.3284 21.6716 28 22.5 28H25.5C26.3284 28 27 27.3284 27 26.5V24.5C27 23.6716 26.3284 23 25.5 23H22.5ZM9.5 34C7.567 34 6 35.567 6 37.5V39.5C6 41.433 7.567 43 9.5 43H12.5C14.433 43 16 41.433 16 39.5V37.5C16 35.567 14.433 34 12.5 34H9.5ZM8 37.5C8 36.6716 8.67157 36 9.5 36H12.5C13.3284 36 14 36.6716 14 37.5V39.5C14 40.3284 13.3284 41 12.5 41H9.5C8.67157 41 8 40.3284 8 39.5V37.5ZM22.5 36C21.6716 36 21 36.6716 21 37.5V39.5C21 40.3284 21.6716 41 22.5 41H25.5C26.3284 41 27 40.3284 27 39.5V37.5C27 36.6716 26.3284 36 25.5 36H22.5ZM44.3481 22.134C42.4349 21.0294 39.9885 21.6849 38.884 23.5981L37.384 26.1962L28.884 40.9186C28.7758 41.106 28.7313 41.3234 28.7572 41.5382L29.3553 46.5023C29.3929 46.8148 29.5755 47.0913 29.8481 47.2487C30.1207 47.4061 30.4514 47.4259 30.7409 47.3023L35.339 45.3382C35.5379 45.2532 35.704 45.106 35.8122 44.9186L44.3122 30.1962L45.8122 27.5981C46.9167 25.6849 46.2612 23.2385 44.3481 22.134ZM40.616 24.5981C41.1683 23.6415 42.3915 23.3137 43.3481 23.866C44.3047 24.4183 44.6324 25.6415 44.0801 26.5981L43.0801 28.3301L39.616 26.3301L40.616 24.5981ZM38.616 28.0622L42.0801 30.0622L34.2468 43.6299L31.1814 44.9393L30.7827 41.6299L38.616 28.0622Z"),
                            Fill = new SolidColorBrush(Colors.Black),
                            Stretch = Stretch.Fill,
                        },
                        TargetType = typeof(SchedulerViewModel) 
            },
        	new NavigationPaneItem() { 
                        Label = Resources.ShellTileViewPage,
                        Path = new Path()
                        {
                            Width = 15,
                            Height = 15,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Data = Geometry.Parse("M0 3.5C0 1.567 1.567 0 3.5 0H10.5C12.433 0 14 1.567 14 3.5V26.5C14 28.433 12.433 30 10.5 30H3.5C1.567 30 0 28.433 0 26.5V3.5ZM3.5 2C2.67157 2 2 2.67157 2 3.5V26.5C2 27.3284 2.67157 28 3.5 28H10.5C11.3284 28 12 27.3284 12 26.5V3.5C12 2.67157 11.3284 2 10.5 2H3.5ZM17 20.5C17 18.567 18.567 17 20.5 17H27.5C29.433 17 31 18.567 31 20.5V27.5C31 29.433 29.433 31 27.5 31H20.5C18.567 31 17 29.433 17 27.5V20.5ZM20.5 19C19.6716 19 19 19.6716 19 20.5V27.5C19 28.3284 19.6716 29 20.5 29H27.5C28.3284 29 29 28.3284 29 27.5V20.5C29 19.6716 28.3284 19 27.5 19H20.5ZM0 36.5C0 34.567 1.567 33 3.5 33H10.5C12.433 33 14 34.567 14 36.5V44.5C14 46.433 12.433 48 10.5 48H3.5C1.567 48 0 46.433 0 44.5V36.5ZM3.5 35C2.67157 35 2 35.6716 2 36.5V44.5C2 45.3284 2.67157 46 3.5 46H10.5C11.3284 46 12 45.3284 12 44.5V36.5C12 35.6716 11.3284 35 10.5 35H3.5ZM20.5 0C18.567 0 17 1.567 17 3.5V10.5C17 12.433 18.567 14 20.5 14H27.5C29.433 14 31 12.433 31 10.5V3.5C31 1.567 29.433 0 27.5 0H20.5ZM19 3.5C19 2.67157 19.6716 2 20.5 2H27.5C28.3284 2 29 2.67157 29 3.5V10.5C29 11.3284 28.3284 12 27.5 12H20.5C19.6716 12 19 11.3284 19 10.5V3.5ZM34 3.5C34 1.567 35.567 0 37.5 0H44.5C46.433 0 48 1.567 48 3.5V15.5C48 17.433 46.433 19 44.5 19H37.5C35.567 19 34 17.433 34 15.5V3.5ZM37.5 2C36.6716 2 36 2.67157 36 3.5V15.5C36 16.3284 36.6716 17 37.5 17H44.5C45.3284 17 46 16.3284 46 15.5V3.5C46 2.67157 45.3284 2 44.5 2H37.5ZM34 25.5C34 23.567 35.567 22 37.5 22H44.5C46.433 22 48 23.567 48 25.5V44.5C48 46.433 46.433 48 44.5 48H37.5C35.567 48 34 46.433 34 44.5V25.5ZM37.5 24C36.6716 24 36 24.6716 36 25.5V44.5C36 45.3284 36.6716 46 37.5 46H44.5C45.3284 46 46 45.3284 46 44.5V25.5C46 24.6716 45.3284 24 44.5 24H37.5ZM20.5 34C18.567 34 17 35.567 17 37.5V44.5C17 46.433 18.567 48 20.5 48H27.5C29.433 48 31 46.433 31 44.5V37.5C31 35.567 29.433 34 27.5 34H20.5ZM19 37.5C19 36.6716 19.6716 36 20.5 36H27.5C28.3284 36 29 36.6716 29 37.5V44.5C29 45.3284 28.3284 46 27.5 46H20.5C19.6716 46 19 45.3284 19 44.5V37.5Z"),
                            Fill = new SolidColorBrush(Colors.Black),
                            Stretch = Stretch.Fill,
                        },
                        TargetType = typeof(TileViewViewModel) 
            },
        };


        public ICommand OptionsMenuItemInvokedCommand => _optionsMenuItemInvokedCommand ?? (_optionsMenuItemInvokedCommand = new RelayCommand(OnOptionsMenuItemInvoked));

        public RelayCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new RelayCommand(OnGoBack, CanGoBack));

        public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(OnLoaded));

        public ICommand UnloadedCommand => _unloadedCommand ?? (_unloadedCommand = new RelayCommand(OnUnloaded));

        public ShellViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SetttingsIconColor = new SolidColorBrush(Colors.Black);
        }

        private void OnLoaded()
        {
            _navigationService.Navigated += OnNavigated;
        }

        private void OnUnloaded()
        {
            _navigationService.Navigated -= OnNavigated;
        }

        private bool CanGoBack()
            => _navigationService.CanGoBack;

        private void OnGoBack()
            => _navigationService.GoBack();

        private void NavigateTo(Type targetViewModel)
        {
            if (targetViewModel != null)
            {
                _navigationService.NavigateTo(targetViewModel.FullName);
            }
        }

        private void OnNavigated(object sender, string viewModelName)
        {
            var item = MenuItems
                        .OfType<NavigationPaneItem>()
                        .FirstOrDefault(i => viewModelName == i.TargetType?.FullName);
            if (item != null)
            {
                SelectedMenuItem = item;
            }

            GoBackCommand.OnCanExecuteChanged();
        }

        private void OnOptionsMenuItemInvoked()
            => NavigateTo(typeof(SettingsViewModel));
    }

    public class NavigationPaneItem
    {
        public string Label { get; set; }
        public Path Path { get; set; }
        public Type TargetType { get; set; }

    }
}
