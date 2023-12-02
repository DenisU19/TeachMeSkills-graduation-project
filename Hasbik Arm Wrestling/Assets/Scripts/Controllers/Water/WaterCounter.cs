using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCounter : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private PlayerStats _playerStats;
    [SerializeField]
    private BusterManager _busterManager;
    [SerializeField]
    private PlayerDataForSave _playerDataForSave;
    [SerializeField]
    private WaterViewRedrawer _waterViewRedrawer;

    private PlayerUpgrade _playerUpgrade;

    public static float MaxWaterCount;

    public static float ÑurrentWaterCount;

    private void Awake()
    {
        _eventBus.OnPlayerDataLoaded += GetSavedWatedData;

        MaxWaterCount = _playerStats.AllStats[PlayerStats.HeroStats.WaterCount];

        ÑurrentWaterCount = MaxWaterCount;

        _waterViewRedrawer.RedrawWaterCount(ÑurrentWaterCount, MaxWaterCount);
    }

    private void Start()
    {
        _eventBus.OnWaterSpended += SpendWaterCount;
        _eventBus.OnWaterAdded += AddWaterCount;
        _eventBus.OnStrengthUpgraded += UpgradeWaterCount;

        _playerUpgrade = FindObjectOfType<MetabolismUpgrade>(true);
    }

    public void AddWaterCount()
    {
        ÑurrentWaterCount = MaxWaterCount;

        _waterViewRedrawer.RedrawWaterCount(ÑurrentWaterCount, MaxWaterCount);
    }

    public void UpgradeWaterCount()
    {
        MaxWaterCount *= _playerStats.AllStatsChangeCoefficients[PlayerStats.HeroStatsForChange.WaterCount];

        ÑurrentWaterCount = MaxWaterCount;

        _waterViewRedrawer.RedrawWaterCount(ÑurrentWaterCount, MaxWaterCount);
    }

    public void SpendWaterCount(float waterSpend)
    {
        ÑurrentWaterCount -= (waterSpend / _playerUpgrade.CurrentUpgradePercentValue / _busterManager.CurrentBusterEffectValue);

        if(ÑurrentWaterCount <= 0)
        {
            ÑurrentWaterCount = 0;
        }

        _waterViewRedrawer.RedrawWaterCount(ÑurrentWaterCount, MaxWaterCount);
    }

    public void GetSavedWatedData()
    {
        ÑurrentWaterCount = _playerDataForSave.CurrentWaterCount;
        MaxWaterCount = _playerDataForSave.MaxWaterCount;

        _waterViewRedrawer.RedrawWaterCount(ÑurrentWaterCount, MaxWaterCount);
    }

    private void OnDestroy()
    {
        _eventBus.OnWaterSpended -= SpendWaterCount;
        _eventBus.OnWaterAdded -= AddWaterCount;
        _eventBus.OnStrengthUpgraded -= UpgradeWaterCount;
        _eventBus.OnPlayerDataLoaded -= GetSavedWatedData;
    }
}
