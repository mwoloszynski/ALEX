﻿using Syncfusion.SfSkinManager;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ALEX.Views.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy StockWindow.xaml
    /// </summary>
    public partial class StockWindow : ChromelessWindow
    {
        public string themeName = App.Current.Properties["Theme"]?.ToString() != null ? App.Current.Properties["Theme"]?.ToString() : "Windows11Light";

        public StockWindow()
        {
            InitializeComponent();
            SfSkinManager.SetTheme(this, new Theme(themeName));
        }
    }
}
