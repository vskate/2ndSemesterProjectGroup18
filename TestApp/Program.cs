using HeatProductionOptimizationApp.Models;

class Program
{
    static void Main(string[] args)
    {
        var assetManager = new AssetManager();
        assetManager.InitializeScenario2Units();

        foreach (var unit in assetManager.GetAllUnits())
        {
            Console.WriteLine($"✅ {unit.Name} | Heat: {unit.MaxHeatOutput} MW | Elec: {unit.ElectricityOutput} | Cost: {unit.CostPerMWh} €/MWh");
        }

        Console.WriteLine("\nAre there4 units above? If yes, we are all good.");
    }
}
