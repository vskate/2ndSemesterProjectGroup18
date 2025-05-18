using HeatProductionOptimizationApp.Models;

class Program
{
    static void Main(string[] args)
    {
        // Load your test CSV
        var elecPrices = SourceDataManager.GetElectricityPrices("TestApp/TestData/heat_demand_with_prices.csv");

        // Load the production units
        var assetManager = new AssetManager();
        assetManager.InitializeScenario2Units();

        var units = assetManager.GetAllUnits();

        // Calculate net cost per unit per hour
        var costMap = CostCalculator.GetNetProductionCosts(units, elecPrices);

        // Print a few cost values for one of the interesting units (e.g., Gas Motor)
        Console.WriteLine("🔥 Net cost per MWh for Gas Motor:");
        foreach (var kvp in costMap["Gas Motor"].Take(5))
        {
            Console.WriteLine($"🕒 {kvp.Key:yyyy-MM-dd HH:mm} → 💸 {kvp.Value:F2} €/MWh");
        }
    }
}
