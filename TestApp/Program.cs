using HeatProductionOptimizationApp.Models;

class Program
{
    static void Main(string[] args)
    {
        var assetManager = new AssetManager();
        assetManager.InitializeScenario2Units();
        var units = assetManager.GetAllUnits();

        var heatDemand = SourceDataManager.LoadHeatData("TestApp/TestData/heat_demand_with_prices.csv");
        var electricityPrices = SourceDataManager.GetElectricityPrices("TestApp/TestData/heat_demand_with_prices.csv");

        var optimizer = new Optimizer();
        optimizer.OptimizeScenario2(units, heatDemand, electricityPrices);
    }
}
