using System;
using System.Collections.Generic;
using System.Linq;

namespace HeatProductionOptimizationApp.Models
{
    //class for asset manager with list of production units
    // Idk if it should be like this
    public class AssetManager
    {
        //list of production units for scenario 1
        public List<ProductionUnit> ProductionUnits = new List<ProductionUnit>
        {
            new ProductionUnit("GB1", 4.0m, 520, 175, 0.9m, 0), //gas boiler
            new ProductionUnit("GB2", 3.0m, 560, 130, 0.7m, 0), //gas boiler
            new ProductionUnit("OB1", 4.0m, 670, 330, 1.5m, 0) //oil boiler
        };
    }

    // here should be image idk how to do it
    public class HeatingArea
    {
        private string _architecture;
        private int _numberOfBuildings;
        private string _cityName;

        public HeatingArea(string architecture, int numberOfBuildings, string cityName)
        {
            _architecture = architecture;
            _numberOfBuildings = numberOfBuildings;
            _cityName = cityName;
        }
    }


    //class for production units with properties from word document
    // image also missing
    public class ProductionUnit
    {
        private string _name;
        private decimal _maxHeat;//MW
        private decimal _productionCost; //DKK/MWh
        private decimal _cO2Emission; //kg/MWh
        private decimal _consuption; //MWh(fuel)/MWh(heat)
        private decimal _maxElectricity; //MW

        public ProductionUnit(string name, decimal maxHeat, decimal productionCost, decimal cO2Emission, decimal consuption, decimal maxElectricity)
        {
            _name = name;
            _maxHeat = maxHeat;
            _productionCost = productionCost;
            _cO2Emission = cO2Emission;
            _consuption = consuption;
            _maxElectricity = maxElectricity;
        }
    }

}