using System;

namespace HeatProductionOptimizationApp.Models;

public class OptimizationResultEntry
{
    public DateTime Timestamp { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public double HeatProduced { get; set; }
    public double CostPerMWh { get; set; }
    public double TotalCost => HeatProduced * CostPerMWh;
}
