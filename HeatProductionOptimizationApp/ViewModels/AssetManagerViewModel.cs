using System.Collections.Generic;
using HeatProductionOptimizationApp.Models;

namespace HeatProductionOptimizationApp.ViewModels;

public partial class AssetManagerViewModel : ViewModelBase
{
    private readonly AssetManager _assetManager = new AssetManager();

    public List<ProductionUnit> Units => _assetManager.GetAllUnits();
}
