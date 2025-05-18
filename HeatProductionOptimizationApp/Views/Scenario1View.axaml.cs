using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using HeatProductionOptimizationApp.ViewModels;

namespace HeatProductionOptimizationApp.Views;

public partial class Scenario1View : UserControl
{
    public Scenario1View()
    {
        InitializeComponent();
        DataContext = new Scenario1ViewModel();
    }
}