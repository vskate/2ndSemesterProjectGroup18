using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using HeatProductionOptimizationApp.ViewModels;

namespace HeatProductionOptimizationApp.Views;

public partial class DataVisulalization : UserControl
{
    public DataVisulalization()
    {
        InitializeComponent();
        DataContext = new DataVisulalizationViewModel();
    }
}