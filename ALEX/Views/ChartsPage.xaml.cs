using System.Collections.ObjectModel;
using System.Windows.Controls;
using ALEX.Models;
using ALEX.ViewModels;
using Syncfusion.SfSkinManager;
namespace ALEX.Views
{
    public partial class ChartsPage : Page
    {
		public string themeName = App.Current.Properties["Theme"]?.ToString()!= null? App.Current.Properties["Theme"]?.ToString(): "Windows11Light";
        public ChartsPage(ChartsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
			SfSkinManager.SetTheme(this, new Theme(themeName));
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var context = this.DataContext as ChartsViewModel;
            if (context != null)
                context.DownloadData();
        }

        private void SfChart_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var test = imports.ItemsSource as ObservableCollection<ChartsModel>;
            test.Add(new ChartsModel { Name = "Test", Imports = 2.2, Exports = 3.3 });
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var test = imports.ItemsSource as ObservableCollection<ChartsModel>;
            test.Add(new ChartsModel { Name = "Test2", Imports = 5.2, Exports = 2.3 });
        }
    }
}
