using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Layout;
using HeatProductionOptimizationApp.ViewModels;

namespace HeatProductionOptimizationApp.Views;

public class ResultDataManagerView : UserControl
{
    public ResultDataManagerView()
    {
        var vm = new ResultDataManagerViewModel();
        vm.LoadResults(); // ✅ Auto-load data as soon as the view is created

        var panel = new StackPanel
        {
            Orientation = Orientation.Vertical,
            Margin = new Thickness(20),
            Spacing = 10,
            DataContext = vm
        };

        panel.Children.Add(new TextBlock { Text = "✅ Results (Demo View)", FontSize = 18 });

        var button = new Button
        {
            Content = "Load Results",
            Width = 150
        };
        button.Bind(Button.CommandProperty, new Binding("LoadResultsCommand"));

        var listBox = new ListBox();
        listBox.Bind(ItemsControl.ItemsSourceProperty, new Binding("Results"));
        listBox.ItemTemplate = new FuncDataTemplate<ResultDataManagerViewModel.ResultRow>((item, _) =>
            new TextBlock { Text = $"{item.Timestamp} - {item.UnitName} - {item.HeatProduced} MWh" }, true);

        panel.Children.Add(button);
        panel.Children.Add(listBox);

        this.Content = panel;
    }
}