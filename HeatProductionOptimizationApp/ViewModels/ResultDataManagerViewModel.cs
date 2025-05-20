using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HeatProductionOptimizationApp.ViewModels;

public partial class ResultDataManagerViewModel : ObservableObject
{
    private List<ResultRow> _rawResults = new();

    [ObservableProperty]
    private ObservableCollection<ResultRow> visibleResults = new();
    public ObservableCollection<string> AllUnits { get; } = new();
    public ObservableCollection<string> SelectedUnits { get; } = new();

    public IRelayCommand LoadResultsCommand { get; }

    public ResultDataManagerViewModel()
    {
        LoadResultsCommand = new RelayCommand(() => LoadResults());
        LoadResults();
    }

    public void LoadResults(string path = "TestApp/output/scenario2_results.csv")
    {
        _rawResults.Clear();
        AllUnits.Clear();
        SelectedUnits.Clear();
        VisibleResults.Clear();

        if (!File.Exists(path))
        {
            Console.WriteLine($"❌ File not found: {path}");
            return;
        }

        var lines = File.ReadAllLines(path).Skip(1);

        foreach (var line in lines)
        {
            var parts = line.Split(',');
            if (parts.Length < 8) continue;

            var row = new ResultRow
            {
                Timestamp = DateTime.Parse(parts[0], CultureInfo.InvariantCulture),
                UnitName = parts[1],
                HeatProduced = double.Parse(parts[2], CultureInfo.InvariantCulture),
                CostPerMWh = double.Parse(parts[3], CultureInfo.InvariantCulture),
                TotalCost = double.Parse(parts[4], CultureInfo.InvariantCulture),
                Electricity = double.Parse(parts[5], CultureInfo.InvariantCulture),
                CO2 = double.Parse(parts[6], CultureInfo.InvariantCulture),
                PrimaryEnergy = double.Parse(parts[7], CultureInfo.InvariantCulture)
            };

            _rawResults.Add(row);
        }

        var unitNames = _rawResults.Select(r => r.UnitName).Distinct();
        foreach (var unit in unitNames)
        {
            AllUnits.Add(unit);
            SelectedUnits.Add(unit);
        }

        ApplyFilter();
    }

    public void ToggleUnit(string unitName)
    {
        if (SelectedUnits.Contains(unitName))
            SelectedUnits.Remove(unitName);
        else
            SelectedUnits.Add(unitName);

        ApplyFilter();
    }

    private void ApplyFilter()
    {
        var filtered = _rawResults
            .Where(r => SelectedUnits.Contains(r.UnitName))
            .ToList();

        VisibleResults = new ObservableCollection<ResultRow>(filtered);
    }

    public class ResultRow
    {
        public DateTime Timestamp { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public double HeatProduced { get; set; }
        public double CostPerMWh { get; set; }
        public double TotalCost { get; set; }
        public double Electricity { get; set; }
        public double CO2 { get; set; }
        public double PrimaryEnergy { get; set; }
    }
}
