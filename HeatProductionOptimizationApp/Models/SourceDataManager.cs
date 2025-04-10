using System;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using HeatProductionOptimizationApp.Models;

namespace HeatProductionOptimizationApp.Models;

public interface ILoader<T>
{
    List<T> Load(string path);
}

public class EnergyPeriod
{
    public DateTime TimeFrom { get; set; }
    public DateTime TimeTo { get; set; }
    public double HeatDemand { get; set; }
    public double ElectricityPrice { get; set; }
    public string? Season { get; set; } // "Winter" or "Summer"
}

public class EnergyPeriodLoader : ILoader<(List<EnergyPeriod> winter, List<EnergyPeriod> summer)>
{
    public List<(List<EnergyPeriod> winter, List<EnergyPeriod> summer)> Load(string path)
    {
        var winterPeriods = new List<EnergyPeriod>();
        var summerPeriods = new List<EnergyPeriod>();

        var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
            MissingFieldFound = null,
            BadDataFound = null
        };

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, config);

        // Skip the first two header rows
        csv.Read(); // Row 1
        csv.Read(); // Row 2

        while (csv.Read())
        {
            try
            {
                var winter = new EnergyPeriod
                {
                    TimeFrom = csv.GetField<DateTime>(1),
                    TimeTo = csv.GetField<DateTime>(2),
                    HeatDemand = csv.GetField<double>(3),
                    ElectricityPrice = csv.GetField<double>(4)
                };
                winterPeriods.Add(winter);
            }
            catch { /* Handle parsing error if needed */ }

            try
            {
                var summer = new EnergyPeriod
                {
                    TimeFrom = csv.GetField<DateTime>(6),
                    TimeTo = csv.GetField<DateTime>(7),
                    HeatDemand = csv.GetField<double>(8),
                    ElectricityPrice = csv.GetField<double>(9)
                };
                summerPeriods.Add(summer);
            }
            catch { /* Handle parsing error if needed */ }
        }

        return new List<(List<EnergyPeriod> winter, List<EnergyPeriod> summer)> { (winterPeriods, summerPeriods) };
    }
}