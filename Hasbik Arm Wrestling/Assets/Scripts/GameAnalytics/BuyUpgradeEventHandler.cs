using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GreenGrey.Analytics;

public class BuyUpgradeEventHandler : MonoBehaviour
{
    public void GetNewUpgradeHandler(string upgradeCategory, int upgradeLevel, int upgradeCost)
    {
        GGAnalytics.Instance.LogEvent("app_upgrades", new Dictionary<string, object>
        {
            ["app_name"] = "Hasbik armwrestling",
            ["app_version"] = "0.1",
            ["app_session_id"] = "0.1",
            ["meta_version"] = "1.3.9",
            ["upgrade_category"] = "Player",
            ["upgrade_id"] = upgradeCategory, // arm exercise, leg exercise, metabolism, popularity
            ["upgrade_level"] = upgradeLevel,
            ["upgrade_cost"] = upgradeCost, // win, fail
            ["upgrade_currency"] = "Gold" //Gold only

        });
    }
}
