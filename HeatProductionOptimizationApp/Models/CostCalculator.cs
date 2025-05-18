using System;
using System.Collections.Generic;

namespace HeatProductionOptimizationApp.Models;

public static class CostCalculator
{
    // This figures out the real heat production cost per unit, per hour — based on electricity prices
    public static Dictionary<string, Dictionary<DateTime, double>> GetNetProductionCosts(
        List<ProductionUnit> units,
        Dictionary<DateTime, double> electricityPrices)
    {
        // This holds cost per unit per timestamp, like: "Gas Motor" → { timestamp → cost }
        var netCosts = new Dictionary<string, Dictionary<DateTime, double>>();

        foreach (var unit in units)
        {
            var unitCosts = new Dictionary<DateTime, double>();

            foreach (var kvp in electricityPrices)
            {
                var timestamp = kvp.Key;
                var elecPrice = kvp.Value;
                double netCost;

                if (unit.ElectricityOutput > 0)
                {
                    // This unit produces electricity, so we earn money
                    netCost = unit.CostPerMWh - (unit.ElectricityOutput * elecPrice);
                }
                else if (unit.ElectricityOutput < 0)
                {
                    // This unit consumes electricity, so we pay extra
                    netCost = unit.CostPerMWh + (-unit.ElectricityOutput * elecPrice);
                }
                else
                {
                    // Regular heat-only unit — nothing fancy
                    netCost = unit.CostPerMWh;
                }

                unitCosts[timestamp] = netCost;
            }

            netCosts[unit.Name] = unitCosts;
        }

        return netCosts;
    }
}