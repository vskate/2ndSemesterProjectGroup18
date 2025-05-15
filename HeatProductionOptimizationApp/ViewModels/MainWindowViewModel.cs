using System;
using System.Collections.Generic;
using System.IO;
using HeatProductionOptimizationApp.Models;

namespace HeatProductionOptimizationApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";

    // Holds the loaded CSV data
    public List<HeatDataEntry> HeatData { get; }

    // Constructor
    public MainWindowViewModel()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "Data", "HeatData.csv");

        if (File.Exists(filePath))
        {
            HeatData = SourceDataManager.LoadHeatData(filePath);
            Console.WriteLine($"✅ Loaded {HeatData.Count} entries from HeatData.csv");

            foreach (var entry in HeatData)
            {
                Console.WriteLine($"{entry.TimeFrom} → {entry.TimeTo}: {entry.HeatDemand} MWh @ {entry.ElectricityPrice} DKK/MWh");
            }

            // Push to shared repository for other modules to access
            DataRepository.HeatData = HeatData;
            Console.WriteLine("📦 Heat data pushed to DataRepository.");
        }
        else
        {
            Console.WriteLine("❌ Could not find HeatData.csv");
            HeatData = new List<HeatDataEntry>();
        }
    }
}