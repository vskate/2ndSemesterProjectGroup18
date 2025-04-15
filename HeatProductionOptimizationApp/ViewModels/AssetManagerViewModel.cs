using System;
using System.Collections.Generic;
using HeatProductionOptimizationApp.Models;

namespace HeatProductionOptimizationApp.ViewModels;

public partial class AssetManagerViewModel : ViewModelBase
{
    public List<ProductionUnit> Units => AssetManager.ProductionUnits;
}