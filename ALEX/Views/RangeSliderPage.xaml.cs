using System;
using System.Windows.Controls;
using ALEX.ViewModels;
using Syncfusion.SfSkinManager;
using Syncfusion.Windows.Tools.Controls;
namespace ALEX.Views
{
    public partial class RangeSliderPage : Page
    {
		public string themeName = App.Current.Properties["Theme"]?.ToString()!= null? App.Current.Properties["Theme"]?.ToString(): "Windows11Light";
        public RangeSliderPage(RangeSliderViewModel viewModel)
        {
            InitializeComponent();		
            DataContext = viewModel;
			SfSkinManager.SetTheme(this, new Theme(themeName));
        }	
    }
}
