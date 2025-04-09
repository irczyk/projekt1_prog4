using System;

namespace WaterTrackerWPF
{
    public class WaterMeasurement
    {
        public DateTime Date { get; set; }
        public double Liters { get; set; }

        public override string ToString()
        {
            return $"{Date.ToShortDateString()} - {Liters} L";
        }
    }
}
