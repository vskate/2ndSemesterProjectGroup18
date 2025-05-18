using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace HeatProductionOptimizationApp.Models;

public static class ResultDataManager
{
    // Saves results to a CSV file
    public static void SaveResultsToCsv(List<OptimizationResultEntry> results, string filePath)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Timestamp,UnitName,HeatProduced,CostPerMWh,TotalCost");

        foreach (var entry in results)
        {
            sb.AppendLine($"{entry.Timestamp:yyyy-MM-dd HH:mm},{entry.UnitName},{entry.HeatProduced},{entry.CostPerMWh.ToString(CultureInfo.InvariantCulture)},{entry.TotalCost.ToString(CultureInfo.InvariantCulture)}");
        }

        File.WriteAllText(filePath, sb.ToString());
    }
}
