using System;
using System.Collections.Generic;
using System.Linq;

namespace HeatProductionOptimizationApp.Models
{
    // Handles all the units and heating areas we're working with
    public class AssetManager
    {
        private readonly List<ProductionUnit> _productionUnits = new();
        private readonly List<HeatingArea> _heatingAreas = new();

        // Add a new unit to the list
        public void AddUnit(ProductionUnit unit)
        {
            _productionUnits.Add(unit);
        }

        // Get a copy of all units (so the list can't be messed with directly)
        public List<ProductionUnit> GetAllUnits()
        {
            return _productionUnits.ToList();
        }

        // Find a unit by its name (useful for quick access)
        public ProductionUnit? FindUnitByName(string name)
        {
            return _productionUnits.FirstOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        // Add a new heating area to the list
        public void AddHeatingArea(HeatingArea area)
        {
            _heatingAreas.Add(area);
        }

        // Same as units, just returns a copy of heating areas
        public List<HeatingArea> GetAllHeatingAreas()
        {
            return _heatingAreas.ToList();
        }

        // Set up the units we need for Scenario 2
        public void InitializeScenario2Units()
        {
            // Basic heat-only boilers
            AddUnit(new ProductionUnit
            {
                Name = "Gas Boiler",
                MaxHeatOutput = 5,
                MinHeatOutput = 0,
                Efficiency = 0.9,
                CostPerMWh = 25
            });

            AddUnit(new ProductionUnit
            {
                Name = "Oil Boiler",
                MaxHeatOutput = 4,
                MinHeatOutput = 0,
                Efficiency = 0.8,
                CostPerMWh = 40
            });

            // Gas motor: makes electricity while producing heat
            AddUnit(new ProductionUnit
            {
                Name = "Gas Motor",
                MaxHeatOutput = 5,
                MinHeatOutput = 0,
                Efficiency = 0.85,
                ElectricityOutput = 3,  // makes 3 MWh electricity
                CostPerMWh = 35
            });

            // Heat pump: uses electricity to make heat
            AddUnit(new ProductionUnit
            {
                Name = "Heat Pump",
                MaxHeatOutput = 4,
                MinHeatOutput = 0,
                Efficiency = 1.0,
                ElectricityOutput = -4, // needs 4 MWh electricity
                CostPerMWh = 20
            });
        }
    }

    // Just holds basic info about a heating area
    public class HeatingArea
    {
        public string Architecture { get; set; }
        public int NumberOfBuildings { get; set; }
        public string CityName { get; set; }

        public HeatingArea(string architecture, int numberOfBuildings, string cityName)
        {
            Architecture = architecture;
            NumberOfBuildings = numberOfBuildings;
            CityName = cityName;
        }
    }
}