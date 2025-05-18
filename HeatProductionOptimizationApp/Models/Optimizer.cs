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
            // optional: clean up this part later since we use Scenario 2 units externally now
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

        // 🔥 Scenario 2: Optimize using net cost per unit per hour
        public void OptimizeScenario2(
            List<ProductionUnit> units,
            List<HeatDataEntry> heatDemand,
            Dictionary<DateTime, double> electricityPrices)
        {
            var netCosts = CostCalculator.GetNetProductionCosts(units, electricityPrices);

            foreach (var demandEntry in heatDemand)
            {
                var hour = demandEntry.TimeFrom;
                var requiredHeat = demandEntry.HeatDemand;

                Console.WriteLine($"\n🕒 {hour:yyyy-MM-dd HH:mm} — Need {requiredHeat} MWh of heat");

                var sortedUnits = units.OrderBy(u => netCosts[u.Name][hour]).ToList();

                foreach (var unit in sortedUnits)
                {
                    if (requiredHeat <= 0)
                        break;

                    var available = unit.MaxHeatOutput;
                    var used = Math.Min(requiredHeat, available);
                    requiredHeat -= used;

                    var costPerMWh = netCosts[unit.Name][hour];
                    var totalCost = used * costPerMWh;

                    Console.WriteLine($"   🔧 {unit.Name} → {used} MWh @ {costPerMWh:F2} €/MWh = {totalCost:F2} €");
                }

                if (requiredHeat > 0)
                {
                    Console.WriteLine($"   ⚠️ WARNING: {requiredHeat} MWh not covered!");
                }
            }
        }
    }
}
