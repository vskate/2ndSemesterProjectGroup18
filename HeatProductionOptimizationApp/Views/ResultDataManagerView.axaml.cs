using System;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Controls.Templates;
using HeatProductionOptimizationApp.ViewModels;

namespace HeatProductionOptimizationApp.Views;

public class ResultDataManagerView : UserControl
{
    private ResultDataManagerViewModel _vm;
    private StackPanel _unitTogglePanel;

    public ResultDataManagerView()
    {
        _vm = new ResultDataManagerViewModel();
        DataContext = _vm;

        var rootPanel = new StackPanel
        {
            Orientation = Orientation.Vertical,
            Margin = new Thickness(20),
            Spacing = 10
        };

        rootPanel.Children.Add(new TextBlock
        {
            Text = "🔥 Scenario 2 Results (with Unit Toggles)",
            FontSize = 20,
            FontWeight = FontWeight.Bold
        });

        _unitTogglePanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 6
        };
        rootPanel.Children.Add(_unitTogglePanel);

        // ✅ Regenerate toggles only AFTER data is loaded
        _vm.AllUnits.CollectionChanged += (_, _) => RebuildToggles();

        // Column headers
        var header = new Grid { ColumnDefinitions = CreateColumns(8) };
        AddCell(header, "Time", 0);
        AddCell(header, "Unit", 1);
        AddCell(header, "Heat (MWh)", 2);
        AddCell(header, "dkk/MWh", 3);
        AddCell(header, "Total €", 4);
        AddCell(header, "Elec", 5);
        AddCell(header, "CO₂", 6);
        AddCell(header, "Primary", 7);
        rootPanel.Children.Add(header);

        // Data list
        var listBox = new ListBox();
        listBox.Bind(ItemsControl.ItemsSourceProperty, new Binding("VisibleResults"));
        listBox.ItemTemplate = new FuncDataTemplate<ResultDataManagerViewModel.ResultRow>((item, _) =>
        {
            var row = new Grid { ColumnDefinitions = CreateColumns(8) };

            AddCell(row, item.Timestamp.ToString("HH:mm"), 0);
            AddCell(row, item.UnitName, 1);
            AddCell(row, item.HeatProduced.ToString("0.##"), 2);
            AddCell(row, item.CostPerMWh.ToString("0.##"), 3);
            AddCell(row, item.TotalCost.ToString("0.##"), 4);
            AddCell(row, item.Electricity.ToString("0.##"), 5);
            AddCell(row, item.CO2.ToString("0.##"), 6);
            AddCell(row, item.PrimaryEnergy.ToString("0.##"), 7);

            return row;
        }, true);

        rootPanel.Children.Add(new ScrollViewer
        {
            Height = 500,
            Content = listBox
        });

        this.Content = rootPanel;

        // Load results after UI is set up
        _vm.LoadResults();
    }

    private void RebuildToggles()
    {
        _unitTogglePanel.Children.Clear();

        foreach (var unit in _vm.AllUnits)
        {
            var toggle = new ToggleButton
            {
                Content = unit,
                IsChecked = true,
                MinWidth = 100,
                Margin = new Thickness(0, 0, 6, 0)
            };

            toggle.IsCheckedChanged += (_, _) => _vm.ToggleUnit(unit);
            _unitTogglePanel.Children.Add(toggle);
        }
    }

    private ColumnDefinitions CreateColumns(int count)
    {
        var columns = new ColumnDefinitions();
        for (int i = 0; i < count; i++)
            columns.Add(new ColumnDefinition(GridLength.Star));
        return columns;
    }

    private void AddCell(Grid grid, string text, int col)
    {
        var tb = new TextBlock
        {
            Text = text,
            Margin = new Thickness(4, 0),
            FontSize = 14
        };
        Grid.SetColumn(tb, col);
        grid.Children.Add(tb);
    }
}
