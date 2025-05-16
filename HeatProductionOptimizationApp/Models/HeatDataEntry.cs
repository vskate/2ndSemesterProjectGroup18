using System;

namespace HeatProductionOptimizationApp.Models
{
    public class HeatDataEntry
    {
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public double HeatDemand { get; set; }
        public double ElectricityPrice { get; set; }
    }
}
