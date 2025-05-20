using System;
using System.Collections.Generic;
using System.IO;
using HeatProductionOptimizationApp.Models;
using HeatProductionOptimizationApp.Views; // Add this line
using HeatProductionOptimizationApp.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;

namespace HeatProductionOptimizationApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";

    // Holds the loaded CSV data
    public List<HeatDataEntry> HeatData { get; }

    [ObservableProperty]
    private UserControl currentView;
    private Stack<UserControl> _BackViewStack = new Stack<UserControl>();

    [ObservableProperty]
    private bool isPaneOpen = false;

    private HomeView _homeView = new HomeView { DataContext = new HomeViewModel() };
    private Scenario1View _scenario1View = new Scenario1View { DataContext = new Scenario1ViewModel() };
    private ResultDataManagerView _scenario2View = new ResultDataManagerView
    {
        DataContext = new ResultDataManagerViewModel()
    };



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
       
        CurrentView = _homeView;
}
    
    [RelayCommand]
    public void NavigateToHome()
    {
        CurrentView = _homeView;
        _BackViewStack.Push(CurrentView);
        
    }

    [RelayCommand]
    public void NavigateToScenario1()
    {
        _BackViewStack.Push(CurrentView);
        CurrentView = _scenario1View;
    }

    [RelayCommand]
    public void NavigateToScenario2()
    {
        _BackViewStack.Push(CurrentView);
        CurrentView = _scenario2View;
    }
}