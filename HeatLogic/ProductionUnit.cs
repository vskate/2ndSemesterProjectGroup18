namespace HeatProductionOptimizationApp.Models;

public class ProductionUnit
{
    public string Name { get; set; } = string.Empty;
    public double MaxHeatOutput { get; set; }  // in MW
    public double Efficiency { get; set; }     // (e.g. 0.85 = 85%)
    public double MinHeatOutput { get; set; }  // in MW

    // new for sc. 2
    public double ElectricityOutput { get; set; } = 0; // + for production, - for consumption
    public double CostPerMWh { get; set; } = 0;        // Cost of producing 1 MWh of heat

    // new for reporting & analysis
    public double CO2PerMWh { get; set; } = 0;             // tons of CO2 per MWh of heat
    public double PrimaryEnergyPerMWh { get; set; } = 0;   // fuel usage per MWh
}
