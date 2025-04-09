using System;
using System.Collections.Generic;
using System.Linq;

namespace HeatProductionOptimizationApp.Models
{
    //class for asset manager with list of production units and heating area
    public class AssetManager
    {
        //list of production units
        public static List<ProductionUnit> ProductionUnits = new List<ProductionUnit>
        {
            new ProductionUnit("GB1", 4.0m, 520, 175, 0.9m, 0), //gas boiler
            new ProductionUnit("GB2", 3.0m, 560, 130, 0.7m, 0), //gas boiler
            new ProductionUnit("OB1", 4.0m, 670, 330, 1.5m, 0) //oil boiler
        };
        public static HeatingArea HeatingArea = new HeatingArea("single district heating network", 1600, "Heatington");

        public static Dictionary<HeatingArea, List<ProductionUnit>> ProductionUnitsByHeatingArea = new Dictionary<HeatingArea, List<ProductionUnit>>()
        {
            {HeatingArea, ProductionUnits}
        };
    
    }
    
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

        //properties
        public string Architecture
        {
            get { return _architecture; }
            set { _architecture = value; }
        }

        public int NumberOfBuildings
        {
            get { return _numberOfBuildings; }
            set { _numberOfBuildings = value; }
        }

        public string CityName
        {
            get { return _cityName; }
            set { _cityName = value; }
        }
    }

    //class for production units with properties from word document
    // image missing
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

        //properties
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public decimal MaxHeat
        {
            get { return _maxHeat; }
            set { _maxHeat = value; }
        }

        public decimal ProductionCost
        {
            get { return _productionCost; }
            set { _productionCost = value; }
        }

        public decimal CO2Emission
        {
            get { return _cO2Emission; }
            set { _cO2Emission = value; }
        }

        public decimal Consuption
        {
            get { return _consuption; }
            set { _consuption = value; }
        }

        public decimal MaxElectricity
        {
            get { return _maxElectricity; }
            set { _maxElectricity = value; }
        }

        
    }

}