using System;
using System.Collections.Generic;
using System.Windows;

namespace projekt
{
    public partial class MainWindow : Window
    {
        private List<Measurement> measurements = new List<Measurement>();
        private DataManager dataManager = new DataManager("data.json");

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            measurements = dataManager.LoadData();
            MeasurementList.Items.Clear();
            foreach (var measurement in measurements)
            {
                MeasurementList.Items.Add($"{measurement.Date}: Woda: {measurement.WaterUsage} m³, Energia: {measurement.EnergyUsage} kWh");
            }
        }

        private void AddMeasurement_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(WaterInput.Text, out double water) && double.TryParse(EnergyInput.Text, out double energy))
            {
                if (water < 0 || energy < 0)
                {
                    MessageBox.Show("Wartości nie mogą być ujemne!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Measurement measurement = new Measurement(DateTime.Now, water, energy);
                measurements.Add(measurement);
                dataManager.SaveData(measurements);
                LoadData();
            }
            else
            {
                MessageBox.Show("Wprowadź poprawne liczby!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveSelectedMeasurement_Click(object sender, RoutedEventArgs e)
        {
            if (MeasurementList.SelectedIndex != -1)
            {
                int selectedIndex = MeasurementList.SelectedIndex;
                var result = MessageBox.Show("Czy na pewno chcesz usunąć wybrany pomiar?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    measurements.RemoveAt(selectedIndex);
                    dataManager.SaveData(measurements);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Najpierw wybierz pomiar do usunięcia!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
