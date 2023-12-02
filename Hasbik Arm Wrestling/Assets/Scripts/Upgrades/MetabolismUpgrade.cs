using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetabolismUpgrade : PlayerUpgrade
{
    public override void Awake()
    {
        base.Awake();

        _eventBus.OnPlayerDataLoaded += GetSavedUpgrade;

        _currentUpgradePercentValue = 1 + (_upgradeSettings.AllUpgrades[UpgradesSettings.PlayerUpgrade.Metabolism] * _upgradeLevel / 100);

        SetNewUpgradeCost();

        RedrawUpgradeInterface();
    }
    public override void Upgrade()
    {
        _upgradeLevel++;

        _currentUpgradePercentValue = 1 + (_upgradeSettings.AllUpgrades[UpgradesSettings.PlayerUpgrade.Metabolism] * _upgradeLevel / 100);

        _eventBus.OnMoneySpended(_upgradeCost);

        _buyUpgradeEventHandler.GetNewUpgradeHandler("Metabolism", _upgradeLevel, _upgradeCost);

        SetNewUpgradeCost();

        base.Upgrade();

        RedrawUpgradeInterface();
    }

    public override void SetNewUpgradeCost()
    {

        _upgradeCost = _upgradeSettings.AllUpgradesStartCost[UpgradesSettings.PlayerUpgrade.Metabolism];

        if (_upgradeLevel > 1)
        {
            for (int i = 0; i < _upgradeLevel; i++)
            {
                _upgradeCost = Mathf.RoundToInt(_upgradeCost * _upgradeSettings.AllUpgradesCostCoefficient[UpgradesSettings.PlayerUpgrade.Metabolism]);
            }
        }
    }


    public override void RedrawUpgradeInterface()
    {
        string currentDescription = $"{_upgradeDescription} {_upgradeSettings.AllUpgrades[UpgradesSettings.PlayerUpgrade.Metabolism] * _upgradeLevel}%";

        _upgradesRedrawer.RedrawUpgardeInterface(currentDescription, $"Level {_upgradeLevel}", $"{_upgradeCost}");
    }

    public void GetSavedUpgrade()
    {
        _upgradeLevel = _playerDataForSave.MetabolismUpgradeLevel;

        _currentUpgradePercentValue = 1 + (_upgradeSettings.AllUpgrades[UpgradesSettings.PlayerUpgrade.Metabolism] * _upgradeLevel / 100);

        SetNewUpgradeCost();

        RedrawUpgradeInterface();
    }

    public void OnDestroy()
    {
        _eventBus.OnPlayerDataLoaded -= GetSavedUpgrade;
    }
}
