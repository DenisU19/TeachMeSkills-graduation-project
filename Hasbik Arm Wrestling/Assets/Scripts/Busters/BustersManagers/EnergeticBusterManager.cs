using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergeticBusterManager : BusterManager
{
    public override void Awake()
    {
        _eventBus.OnPlayerDataLoaded += GetSavedBusterData;

        _busterEffectValue = _bustersSettings.AllBusters[BustersSettings.BusterType.EnergeticBuster].EffectValue;

        _busterActivityTime = _bustersSettings.AllBusters[BustersSettings.BusterType.EnergeticBuster].ActivityTime;

        base.Awake();
    }
    private void Start()
    {
        _eventBus.OnEnergeticBusterCollected += AddBuster;
    }

    public override void SpendBuster()
    {
        base.SpendBuster();

        _eventBus.OnPlayerVisualEffectStart?.Invoke(PlayerVisualEffectsData.GameEvent.UseEnergeticBuster);
    }

    public void GetSavedBusterData()
    {
        _currentBusterCount = _playerDataForSave.EnergeticBusterCount;

        _busterViewDrawer.RedrawBusterView(_currentBusterCount);
    }

    private void OnDestroy()
    {
        _eventBus.OnEnergeticBusterCollected -= AddBuster;
        _eventBus.OnPlayerDataLoaded -= GetSavedBusterData;
    }
}
