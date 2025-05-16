using System;
using System.Collections.Generic;
using System.Linq;
using HeatProductionOptimizationApp.Models;

namespace HeatProductionOptimizationApp.Models
{
    public class Optimizer
    {
        private readonly AssetManager _assetManager = new AssetManager();

        public Optimizer()
        {
            _assetManager.AddUnit(new ProductionUnit
            {
                Name = "Boiler1",
                MaxHeatOutput = 10.0,
                Efficiency = 0.9,
                MinHeatOutput = 2.0
            });

            _assetManager.AddUnit(new ProductionUnit
            {
                Name = "CHP1",
                MaxHeatOutput = 20.0,
                Efficiency = 0.85,
                MinHeatOutput = 5.0
            });
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
    }
}
