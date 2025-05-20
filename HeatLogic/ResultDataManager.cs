using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace HeatProductionOptimizationApp.Models;

public static class ResultDataManager
{
    // Writes all result entries to a CSV file
    public static void SaveResultsToCsv(List<OptimizationResultEntry> results, string filePath)
    {
        var sb = new StringBuilder();

        // Updated header row with new fields
        sb.AppendLine("Timestamp,UnitName,HeatProduced,CostPerMWh,TotalCost,Electricity,CO2,PrimaryEnergy");

        foreach (var entry in results)
        {
            sb.AppendLine($"{entry.Timestamp:yyyy-MM-dd HH:mm},{entry.UnitName},{entry.HeatProduced},{entry.CostPerMWh.ToString(CultureInfo.InvariantCulture)},{entry.TotalCost.ToString(CultureInfo.InvariantCulture)},{entry.Electricity},{entry.CO2},{entry.PrimaryEnergy}");
        }

        File.WriteAllText(filePath, sb.ToString());
    }
}
