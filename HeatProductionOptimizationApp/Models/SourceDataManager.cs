using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace HeatProductionOptimizationApp.Models;

public static class SourceDataManager
{
    // Loads heat demand + electricity price per hour from CSV
    public static List<HeatDataEntry> LoadHeatData(string path)
    {
        var entries = new List<HeatDataEntry>();
        var lines = File.ReadAllLines(path);

        for (int i = 1; i < lines.Length; i++) // skip the header
        {
            var parts = lines[i].Split(',');

            entries.Add(new HeatDataEntry
            {
                TimeFrom = DateTime.Parse(parts[0], CultureInfo.InvariantCulture),
                TimeTo = DateTime.Parse(parts[1], CultureInfo.InvariantCulture),
                HeatDemand = double.Parse(parts[2], CultureInfo.InvariantCulture),
                ElectricityPrice = double.Parse(parts[3], CultureInfo.InvariantCulture)
            });
        }

        return entries;
    }

    // Gives back just the electricity prices per hour, so we can use them separately if needed
    public static Dictionary<DateTime, double> GetElectricityPrices(string path)
    {
        var prices = new Dictionary<DateTime, double>();
        var lines = File.ReadAllLines(path);

        for (int i = 1; i < lines.Length; i++) // skip the header
        {
            var parts = lines[i].Split(',');

            if (DateTime.TryParse(parts[0], out var timeFrom) &&
                double.TryParse(parts[3], out var price)) // column 3 = ElectricityPrice
            {
                prices[timeFrom] = price;
            }
        }

        return prices;
    }
}
