using System;
using System.Linq;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;

namespace WaterTrackerWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RefreshList();
        }

        private void AddMeasurement_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(LitersInput.Text, out double liters) || liters < 0)
            {
                MessageBox.Show("Podaj prawidłową, dodatnią liczbę litrów.", "Błąd");
                return;
            }

            if (DateInput.SelectedDate == null)
            {
                MessageBox.Show("Wybierz datę.");
                return;
            }

            var measurement = new WaterMeasurement
            {
                Date = DateInput.SelectedDate.Value,
                Liters = liters
            };

            DataManager.Measurements.Add(measurement);
            LitersInput.Clear();
            RefreshList();
        }

        private void RemoveMeasurement_Click(object sender, RoutedEventArgs e)
        {
            if (MeasurementList.SelectedItem is WaterMeasurement selected)
            {
                DataManager.Measurements.Remove(selected);
                RefreshList();
            }
            else
            {
                MessageBox.Show("Zaznacz pomiar do usunięcia.");
            }
        }

        private void RefreshList()
        {
            MeasurementList.ItemsSource = null;
            MeasurementList.ItemsSource = DataManager.Measurements.OrderBy(m => m.Date).ToList();
            UpdateChart();
        }

        private void UpdateChart()
        {
            var sorted = DataManager.Measurements.OrderBy(m => m.Date).ToList();
            var values = new ChartValues<double>(sorted.Select(m => m.Liters));
            var labels = sorted.Select(m => m.Date.ToString("dd.MM")).ToArray();

            WaterChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Zużycie wody",
                    Values = values,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 10
                }
            };

            WaterChart.AxisX[0].Labels = labels;
        }
    }
}
