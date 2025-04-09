using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using HeatProductionOptimizationApp.ViewModels;

namespace HeatProductionOptimizationApp.Views;

public partial class Optimizer : UserControl
{
    public Optimizer()
    {
        InitializeComponent();
        DataContext = new OptimizerViewModel();
    }
}