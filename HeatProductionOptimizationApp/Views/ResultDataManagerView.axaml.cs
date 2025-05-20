using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;

namespace HeatProductionOptimizationApp.Views;

public class ResultDataManagerView : UserControl
{
    private Dictionary<string, bool> _unitVisibility = new();
    private List<SimpleResultRow> _allRows = new();

    private StackPanel _rowsPanel;

    public ResultDataManagerView()
    {
        var rootPanel = new StackPanel
        {
            Orientation = Orientation.Vertical,
            Margin = new Thickness(20),
            Spacing = 10
        };

        // Title
        rootPanel.Children.Add(new TextBlock
        {
            Text = "🔥 Scenario 2 Results",
            FontSize = 20,
            FontWeight = FontWeight.Bold
        });

        // Load CSV
        var path = "TestApp/output/scenario2_results.csv";
        if (!File.Exists(path))
        {
            rootPanel.Children.Add(new TextBlock { Text = "❌ CSV file not found." });
            Content = rootPanel;
            return;
        }

        _allRows = File.ReadAllLines(path)
            .Skip(1)
            .Select(line => SimpleResultRow.FromCsv(line))
            .Where(row => row != null)
            .ToList()!;

        // Create unique unit list
        var units = _allRows.Select(r => r.UnitName).Distinct().ToList();
        foreach (var unit in units)
            _unitVisibility[unit] = true;

        // Toggle buttons
        var togglePanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 6
        };

        foreach (var unit in units)
        {
            var toggle = new ToggleButton
            {
                Content = unit,
                IsChecked = true,
                MinWidth = 100,
                Margin = new Thickness(0, 0, 6, 0)
            };

            toggle.Checked += (_, _) => { _unitVisibility[unit] = true; RefreshRows(); };
            toggle.Unchecked += (_, _) => { _unitVisibility[unit] = false; RefreshRows(); };

            togglePanel.Children.Add(toggle);
        }

        rootPanel.Children.Add(togglePanel);

        // Column headers
        var header = new Grid { ColumnDefinitions = CreateColumns(8) };
        AddHeaderCell(header, "Time", 0);
        AddHeaderCell(header, "Unit", 1);
        AddHeaderCell(header, "Heat", 2);
        AddHeaderCell(header, "€/MWh", 3);
        AddHeaderCell(header, "Total", 4);
        AddHeaderCell(header, "Elec", 5);
        AddHeaderCell(header, "CO₂", 6);
        AddHeaderCell(header, "Prim", 7);
        rootPanel.Children.Add(header);

        // Rows container
        _rowsPanel = new StackPanel
        {
            Orientation = Orientation.Vertical,
            Spacing = 2
        };

        var scroll = new ScrollViewer
        {
            Height = 500,
            Content = _rowsPanel
        };

        rootPanel.Children.Add(scroll);
        this.Content = rootPanel;

        RefreshRows();
    }

    private void RefreshRows()
    {
        _rowsPanel.Children.Clear();

        foreach (var row in _allRows)
        {
            if (!_unitVisibility.TryGetValue(row.UnitName, out var visible) || !visible)
                continue;

            var grid = new Grid { ColumnDefinitions = CreateColumns(8) };

            AddCell(grid, row.Timestamp.ToString("HH:mm"), 0);
            AddCell(grid, row.UnitName, 1);
            AddCell(grid, row.HeatProduced.ToString("0.##"), 2);
            AddCell(grid, row.CostPerMWh.ToString("0.##"), 3);
            AddCell(grid, row.TotalCost.ToString("0.##"), 4);
            AddCell(grid, row.Electricity.ToString("0.##"), 5);
            AddCell(grid, row.CO2.ToString("0.##"), 6);
            AddCell(grid, row.PrimaryEnergy.ToString("0.##"), 7);

            _rowsPanel.Children.Add(grid);
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

    private void AddHeaderCell(Grid grid, string text, int col)
    {
        var tb = new TextBlock
        {
            Text = text,
            Margin = new Thickness(4, 0),
            FontSize = 14,
            FontWeight = FontWeight.Bold
        };
        Grid.SetColumn(tb, col);
        grid.Children.Add(tb);
    }

    private class SimpleResultRow
    {
        public DateTime Timestamp;
        public string UnitName = "";
        public double HeatProduced;
        public double CostPerMWh;
        public double TotalCost;
        public double Electricity;
        public double CO2;
        public double PrimaryEnergy;

        public static SimpleResultRow? FromCsv(string line)
        {
            var parts = line.Split(',');
            if (parts.Length < 8) return null;

            try
            {
                return new SimpleResultRow
                {
                    Timestamp = DateTime.Parse(parts[0], CultureInfo.InvariantCulture),
                    UnitName = parts[1],
                    HeatProduced = double.Parse(parts[2], CultureInfo.InvariantCulture),
                    CostPerMWh = double.Parse(parts[3], CultureInfo.InvariantCulture),
                    TotalCost = double.Parse(parts[4], CultureInfo.InvariantCulture),
                    Electricity = double.Parse(parts[5], CultureInfo.InvariantCulture),
                    CO2 = double.Parse(parts[6], CultureInfo.InvariantCulture),
                    PrimaryEnergy = double.Parse(parts[7], CultureInfo.InvariantCulture)
                };
            }
            catch
            {
                return null;
            }
        }
    }
}