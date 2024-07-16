using System.Windows.Controls;
using ALEX.ViewModels;
using Syncfusion.SfSkinManager;
namespace ALEX.Views
{
    public partial class KanbanPage : Page
    {
		public string themeName = App.Current.Properties["Theme"]?.ToString()!= null? App.Current.Properties["Theme"]?.ToString(): "Windows11Light";
        public KanbanPage(KanbanViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
			SfSkinManager.SetTheme(this, new Theme(themeName));
        }
    }
}
