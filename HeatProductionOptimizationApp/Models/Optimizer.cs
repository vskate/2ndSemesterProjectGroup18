using System;
using System.Collections.Generic;
using System.Linq;

namespace HeatProductionOptimizationApp.Models
{
    public class Optimizer
    {
        private readonly AssetManager _assetManager = new AssetManager();

        public Optimizer()
        {
            // optional: you can remove this if units are initialized elsewhere
        }

        public List<ProductionUnit> GetProductionUnits()
        {
            return _assetManager.GetAllUnits();
        }

        public ProductionUnit? FindBestProductionUnit()
        {
            var units = _assetManager.GetAllUnits();
            if (units.Count == 0)
                return null;

            return units.OrderByDescending(u => u.Efficiency).FirstOrDefault();
        }

        public void OptimizeScenario2(
            List<ProductionUnit> units,
            List<HeatDataEntry> heatDemand,
            Dictionary<DateTime, double> electricityPrices)
        {
            var results = new List<OptimizationResultEntry>();
            var netCosts = CostCalculator.GetNetProductionCosts(units, electricityPrices);

            foreach (var demandEntry in heatDemand)
            {
                var hour = demandEntry.TimeFrom;
                var requiredHeat = demandEntry.HeatDemand;

                var sortedUnits = units.OrderBy(u => netCosts[u.Name][hour]).ToList();

                foreach (var unit in sortedUnits)
                {
                    if (requiredHeat <= 0)
                        break;

                    var available = unit.MaxHeatOutput;
                    var used = Math.Min(requiredHeat, available);
                    requiredHeat -= used;

                    var costPerMWh = netCosts[unit.Name][hour];

                    // 📝 Create and store result for this unit in this hour
                    results.Add(new OptimizationResultEntry
                    {
                        Timestamp = hour,
                        UnitName = unit.Name,
                        HeatProduced = used,
                        CostPerMWh = costPerMWh,

                        // NEW: populate extra values
                        Electricity = unit.ElectricityOutput * used,           // total electricity produced/used
                        CO2 = unit.CO2PerMWh * used,                           // total CO2 emitted
                        PrimaryEnergy = unit.PrimaryEnergyPerMWh * used        // total fuel used
                    });
                }

                if (requiredHeat > 0)
                {
                    Console.WriteLine($"⚠️ {requiredHeat} MWh not covered at {hour}");
                }
            }

            ResultDataManager.SaveResultsToCsv(results, "TestApp/output/scenario2_results.csv");
            Console.WriteLine("\n✅ Results saved to TestApp/output/scenario2_results.csv");
        }
    }
}
