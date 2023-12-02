using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GreenGrey.Analytics;

public class StrengthUpgradeEventHandler : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private RoomGenerator _roomGenerator;

    public void UpgradeLevelEventHandler(int newLevel)
    {
        GGAnalytics.Instance.LogEvent("app_player_level", new Dictionary<string, object>
        {
            ["app_name"] = "Hasbik armwrestling",
            ["app_version"] = "0.1",
            ["app_session_id"] = "0.1",
            ["meta_version"] = "1.3.9",
            ["level_id"] = RoomGenerator._roomCount, 
            ["player_level"] = newLevel
        });
    }
}
