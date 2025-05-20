using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HeatProductionOptimizationApp.ViewModels;

public partial class ResultDataManagerViewModel : ObservableObject
{
    private List<ResultRow> _rawResults = new();

    public ObservableCollection<ResultRow> VisibleResults { get; } = new();
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

            _rawResults.Add(new ResultRow
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

        foreach (var unit in _rawResults.Select(r => r.UnitName).Distinct())
        {
            AllUnits.Add(unit);
            SelectedUnits.Add(unit);
        }

        ApplyFilter();
    }

    public void ToggleUnit(string unit)
    {
        if (SelectedUnits.Contains(unit))
            SelectedUnits.Remove(unit);
        else
            SelectedUnits.Add(unit);

        ApplyFilter();
    }

    private void ApplyFilter()
    {
        VisibleResults.Clear();

        foreach (var row in _rawResults)
        {
            if (SelectedUnits.Contains(row.UnitName))
                VisibleResults.Add(row);
        }
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
