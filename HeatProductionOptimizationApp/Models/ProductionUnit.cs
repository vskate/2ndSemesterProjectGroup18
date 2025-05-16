namespace HeatProductionOptimizationApp.Models;

public class ProductionUnit
{
    public string Name { get; set; } = string.Empty;
    public double MaxHeatOutput { get; set; }  // in MW
    public double Efficiency { get; set; }     // (for ex 0.85 = 85%)
    public double MinHeatOutput { get; set; }  // in MW
}
