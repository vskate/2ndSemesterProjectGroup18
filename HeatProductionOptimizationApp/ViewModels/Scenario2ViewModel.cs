using System;
using System.Collections.ObjectModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using ReactiveUI;

namespace HeatProductionOptimizationApp.ViewModels;

public class Scenario2ViewModel : ReactiveObject
{
    private ObservableCollection<string> _seasons = new()
    {
        "Winter",
        "Summer"
    };
    public ObservableCollection<string> Seasons
    {
        get => _seasons;
        set => this.RaiseAndSetIfChanged(ref _seasons, value);
    }

    private string _selectedSeason = "Winter";
    public string SelectedSeason
    {
        get => _selectedSeason;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedSeason, value);
            UpdateCurrentSeries();
        }
    }

    public ISeries[] Scenario2Winter { get; } =
    [
        new StackedAreaSeries<double>([3, 2, 3, 5, 3, 4, 6]),
        new StackedAreaSeries<double>([6, 5, 6, 3, 8, 5, 2]),
        new StackedAreaSeries<double>([4, 8, 2, 8, 9, 5, 3])
    ];

    public ISeries[] Scenario2Summer { get; } =
    [
        new StackedAreaSeries<double>([1, 1, 2, 1, 1, 2, 1]),
        new StackedAreaSeries<double>([5, 5, 5, 5, 5, 5, 5]),
        new StackedAreaSeries<double>([3, 3, 3, 3, 3, 3, 3])
    ];

    private ISeries[] _currentSeries;
    public ISeries[] CurrentSeries
    {
        get => _currentSeries;
        set => this.RaiseAndSetIfChanged(ref _currentSeries, value);
    }

    public Scenario2ViewModel()
    {
        // Initialize with Winter graph
        CurrentSeries = Scenario2Winter;
    }

    private void UpdateCurrentSeries()
    {
        CurrentSeries = SelectedSeason switch
        {
            "Winter" => Scenario2Winter,
            "Summer" => Scenario2Summer,
            _ => null
        };
    }
}
