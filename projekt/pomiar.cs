using System;

namespace projekt
{
    public class Measurement
    {
        public DateTime Date { get; set; }
        public double WaterUsage { get; set; }
        public double EnergyUsage { get; set; }

        public Measurement(DateTime date, double waterUsage, double energyUsage)
        {
            Date = date;
            WaterUsage = waterUsage;
            EnergyUsage = energyUsage;
        }
    }
}