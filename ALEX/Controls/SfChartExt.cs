using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ALEX.Controls
{
    public class SfChartExt : SfChart
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(object), typeof(SfChartExt), new PropertyMetadata(null, OnPropertyChanged));
        public static readonly DependencyProperty SeriesTemplateProperty = DependencyProperty.Register("SeriesTemplate", typeof(DataTemplate), typeof(SfChartExt), new PropertyMetadata(null, OnPropertyChanged));

        //Gets or sets the ItemsSource of collection of collections.
        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        //Gets or sets the template for the series to be generated.
        public DataTemplate SeriesTemplate
        {
            get { return (DataTemplate)GetValue(SeriesTemplateProperty); }
            set { SetValue(SeriesTemplateProperty, value); }
        }
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SfChartExt).GenerateSeries();
        }
        //Generate the series per the counts in the itemssource.
        private void GenerateSeries()
        {
            if (Source == null || SeriesTemplate == null)
                return;
            var commonItemsSource = (Source as IEnumerable).GetEnumerator();
            while (commonItemsSource.MoveNext())
            {
                ChartSeries series = SeriesTemplate.LoadContent() as ChartSeries;
                series.DataContext = commonItemsSource.Current;
                Series.Add(series);
            }
        }
    }
}
