using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace HeatProductionOptimizationApp.Models;

public static class SourceDataManager
{
    public static List<HeatDataEntry> LoadHeatData(string path)
    {
        var entries = new List<HeatDataEntry>();
        var lines = File.ReadAllLines(path);

        for (int i = 1; i < lines.Length; i++) // Skip header
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
}