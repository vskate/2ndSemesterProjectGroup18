using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HeatProductionOptimizationApp.ViewModels;

public partial class ResultDataManagerViewModel : ObservableObject
{
    public ObservableCollection<ResultRow> Results { get; } = new();

    public IRelayCommand LoadResultsCommand { get; }

    public ResultDataManagerViewModel()
    {
        LoadResultsCommand = new RelayCommand(() => LoadResults());
        LoadResults(); // Optional: auto-load on startup
    }

    public void LoadResults(string path = "TestApp/output/scenario2_results.csv")
    {
        Results.Clear();

        if (!File.Exists(path))
            return;

        var lines = File.ReadAllLines(path).Skip(1); // Skip header

        foreach (var line in lines)
        {
            var parts = line.Split(',');

            if (parts.Length < 8)
                continue;

            Results.Add(new ResultRow
            {
                Timestamp = DateTime.Parse(parts[0], CultureInfo.InvariantCulture),
                UnitName = parts[1],
                HeatProduced = double.Parse(parts[2], CultureInfo.InvariantCulture),
                CostPerMWh = double.Parse(parts[3], CultureInfo.InvariantCulture),
                TotalCost = double.Parse(parts[4], CultureInfo.InvariantCulture),
                Electricity = double.Parse(parts[5], CultureInfo.InvariantCulture),
                CO2 = double.Parse(parts[6], CultureInfo.InvariantCulture),
                PrimaryEnergy = double.Parse(parts[7], CultureInfo.InvariantCulture)
            });
        }
    }

    public class ResultRow
    {
        public DateTime Timestamp { get; set; }
        public string UnitName { get; set; }
        public double HeatProduced { get; set; }
        public double CostPerMWh { get; set; }
        public double TotalCost { get; set; }
        public double Electricity { get; set; }
        public double CO2 { get; set; }
        public double PrimaryEnergy { get; set; }
    }
}
