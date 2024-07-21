using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ALEX.Contracts.Services;
using ALEX.Helpers;
using ALEX.Models;
using ALEX.Views.Windows;
using Syncfusion.Windows.Shared;

namespace ALEX.ViewModels
{
    public class ChartsViewModel : Observable
    {
        private ICommand _testCommand;

        public ICommand TestCommand { get { return _testCommand; } }

        public ObservableCollection<ChartsModel> Data { get;}

        public ObservableCollection<ChartsModel> SalesData { get; }

        public ChartsViewModel()
        {
            _testCommand = new RelayCommand(AddStockWindow);
            this.SalesData = new ObservableCollection<ChartsModel>();
            this.Data = new ObservableCollection<ChartsModel>();
        }

        public void DownloadData()
        {
            SalesData.Add(new ChartsModel() { Name = "Product A", SalesRate = 25 });
            SalesData.Add(new ChartsModel() { Name = "Product B", SalesRate = 17 });
            SalesData.Add(new ChartsModel() { Name = "Product C", SalesRate = 30 });
            SalesData.Add(new ChartsModel() { Name = "Product D", SalesRate = 18 });
            SalesData.Add(new ChartsModel() { Name = "Product E", SalesRate = 10 });
            SalesData.Add(new ChartsModel() { Name = "Product F", SalesRate = 21 });

            Data.Add(new ChartsModel() { Name = "Product A", Imports = 2.2, Exports = 1.2 });
            Data.Add(new ChartsModel() { Name = "Product B", Imports = 2.4, Exports = 1.3 });
            Data.Add(new ChartsModel() { Name = "Product C", Imports = 3, Exports = 1.5 });
            Data.Add(new ChartsModel() { Name = "Product D", Imports = 3.3, Exports = 2.2 });
        }

        private void AddStockWindow()
        {
            StockWindow window = new StockWindow();
            window.ShowDialog();
        }
    }
}
