using System;

namespace HeatProductionOptimizationApp.Models;

public class OptimizationResultEntry
{
    public DateTime Timestamp { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public double HeatProduced { get; set; }
    public double CostPerMWh { get; set; }
    public double TotalCost => HeatProduced * CostPerMWh;

    // new: extra result info
    public double Electricity { get; set; }        // in MWh (+ = produced, - = consumed)
    public double CO2 { get; set; }                // total CO2 emitted (tons)
    public double PrimaryEnergy { get; set; }      // total fuel used (arbitrary unit)
}
