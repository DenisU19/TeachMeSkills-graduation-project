using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaBusterManager : BusterManager
{
    public override void Awake()
    {
        _eventBus.OnPlayerDataLoaded += GetSavedBusterData;

        _busterEffectValue = _bustersSettings.AllBusters[BustersSettings.BusterType.SodaBuster].EffectValue;

        _busterActivityTime = _bustersSettings.AllBusters[BustersSettings.BusterType.SodaBuster].ActivityTime;

        base.Awake();
    }
    private void Start()
    {
        _eventBus.OnSodaBusterCollected += AddBuster;
    }

    public override void SpendBuster()
    {
        base.SpendBuster();

        _eventBus.OnPlayerVisualEffectStart?.Invoke(PlayerVisualEffectsData.GameEvent.UseSodaBuster);
    }

    public void GetSavedBusterData()
    {
        _currentBusterCount = _playerDataForSave.SodaBusterCount;

        _busterViewDrawer.RedrawBusterView(_currentBusterCount);
    }

    private void OnDestroy()
    {
        _eventBus.OnSodaBusterCollected -= AddBuster;
        _eventBus.OnPlayerDataLoaded -= GetSavedBusterData;
    }
}
