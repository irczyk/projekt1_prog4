using System.Collections.Generic;

namespace WaterTrackerWPF
{
    public static class DataManager
    {
        public static List<WaterMeasurement> Measurements { get; } = new();
    }
}
