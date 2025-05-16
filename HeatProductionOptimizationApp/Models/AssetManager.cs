using System;
using System.Collections.Generic;
using System.Linq;

namespace HeatProductionOptimizationApp.Models
{
    // This manages a list of production units and heating areas
    public class AssetManager
    {
        private readonly List<ProductionUnit> _productionUnits = new();
        private readonly List<HeatingArea> _heatingAreas = new();

        // Add a production unit
        public void AddUnit(ProductionUnit unit)
        {
            _productionUnits.Add(unit);
        }

        // Get all production units
        public List<ProductionUnit> GetAllUnits()
        {
            return _productionUnits.ToList(); // return a copy for safety
        }

        // Find a unit by name
        public ProductionUnit? FindUnitByName(string name)
        {
            return _productionUnits.FirstOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        // Add a heating area
        public void AddHeatingArea(HeatingArea area)
        {
            _heatingAreas.Add(area);
        }

        // Get all heating areas
        public List<HeatingArea> GetAllHeatingAreas()
        {
            return _heatingAreas.ToList();
        }
    }

    // Represents one heating area
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
} // ✅ FINAL CLOSING BRACE for namespace!
