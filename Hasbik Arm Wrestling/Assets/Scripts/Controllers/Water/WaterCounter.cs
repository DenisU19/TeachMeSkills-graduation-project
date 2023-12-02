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

    public static float �urrentWaterCount;

    private void Awake()
    {
        _eventBus.OnPlayerDataLoaded += GetSavedWatedData;

        MaxWaterCount = _playerStats.AllStats[PlayerStats.HeroStats.WaterCount];

        �urrentWaterCount = MaxWaterCount;

        _waterViewRedrawer.RedrawWaterCount(�urrentWaterCount, MaxWaterCount);
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
        �urrentWaterCount = MaxWaterCount;

        _waterViewRedrawer.RedrawWaterCount(�urrentWaterCount, MaxWaterCount);
    }

    public void UpgradeWaterCount()
    {
        MaxWaterCount *= _playerStats.AllStatsChangeCoefficients[PlayerStats.HeroStatsForChange.WaterCount];

        �urrentWaterCount = MaxWaterCount;

        _waterViewRedrawer.RedrawWaterCount(�urrentWaterCount, MaxWaterCount);
    }

    public void SpendWaterCount(float waterSpend)
    {
        �urrentWaterCount -= (waterSpend / _playerUpgrade.CurrentUpgradePercentValue / _busterManager.CurrentBusterEffectValue);

        if(�urrentWaterCount <= 0)
        {
            �urrentWaterCount = 0;
        }

        _waterViewRedrawer.RedrawWaterCount(�urrentWaterCount, MaxWaterCount);
    }

    public void GetSavedWatedData()
    {
        �urrentWaterCount = _playerDataForSave.CurrentWaterCount;
        MaxWaterCount = _playerDataForSave.MaxWaterCount;

        _waterViewRedrawer.RedrawWaterCount(�urrentWaterCount, MaxWaterCount);
    }

    private void OnDestroy()
    {
        _eventBus.OnWaterSpended -= SpendWaterCount;
        _eventBus.OnWaterAdded -= AddWaterCount;
        _eventBus.OnStrengthUpgraded -= UpgradeWaterCount;
        _eventBus.OnPlayerDataLoaded -= GetSavedWatedData;
    }
}
