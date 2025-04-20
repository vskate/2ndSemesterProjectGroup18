using System.Collections.Generic;

namespace HeatProductionOptimizationApp.Models;

public static class DataRepository
{
    // Shared storage for HeatData
    public static List<HeatDataEntry> HeatData { get; set; } = new();
}