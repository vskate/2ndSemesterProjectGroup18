using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using HeatProductionOptimizationApp.ViewModels;

namespace HeatProductionOptimizationApp.Views;

public partial class AssetManager : UserControl
{
    public AssetManager()
    {
        InitializeComponent();
        DataContext = new AssetManagerViewModel();
    }
}