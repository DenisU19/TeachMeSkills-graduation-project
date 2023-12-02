using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarBusterManager : BusterManager
{
    public override void Awake()
    {
        _eventBus.OnPlayerDataLoaded += GetSavedBusterData;

        _busterEffectValue = _bustersSettings.AllBusters[BustersSettings.BusterType.BarBuster].EffectValue;

        _busterActivityTime = _bustersSettings.AllBusters[BustersSettings.BusterType.BarBuster].ActivityTime;

        base.Awake();
    }
    private void Start()
    {
        _eventBus.OnBarBusterCollected += AddBuster;
    }

    public override void SpendBuster()
    {
        base.SpendBuster();

        _eventBus.OnPlayerVisualEffectStart?.Invoke(PlayerVisualEffectsData.GameEvent.UseBarBuster);
    }

    public void GetSavedBusterData()
    {
        _currentBusterCount = _playerDataForSave.BarBusterCount;

        _busterViewDrawer.RedrawBusterView(_currentBusterCount);
    }

    private void OnDestroy()
    {
        _eventBus.OnBarBusterCollected -= AddBuster;
        _eventBus.OnPlayerDataLoaded -= GetSavedBusterData;
    }
}
