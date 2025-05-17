using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using HeatProductionOptimizationApp.ViewModels;


namespace HeatProductionOptimizationApp.Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
        DataContext = new HomeViewModel ();
    }
}