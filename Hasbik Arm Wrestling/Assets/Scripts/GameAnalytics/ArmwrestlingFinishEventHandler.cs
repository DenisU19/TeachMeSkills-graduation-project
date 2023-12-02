using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GreenGrey.Analytics;

public class ArmwrestlingFinishEventHandler : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private StartDelayTimer _armwrestlingGameTimer;

    private int _roomTryNumber;

    private void Awake()
    {
        _eventBus.OnRoomCompleted += AddTryNumer;
        _eventBus.OnQTECompleted += ResetTryNumbers;
        _eventBus.OnQTEFailed += FailFinishArmwrestlingEventHandler;
    }

    public void AddTryNumer()
    {
        _roomTryNumber++;
    }

    public void ResetTryNumbers()
    {
        WinFinishArmwrestlingEventHandler();
        _roomTryNumber = 0;
    }
    public void WinFinishArmwrestlingEventHandler()
    {
        GGAnalytics.Instance.LogEvent("app_level_end", new Dictionary<string, object>
        {
            ["app_name"] = "Hasbik armwrestling",
            ["app_version"] = "0.1",
            ["app_session_id"] = "0.1",
            ["meta_version"] = "1.3.9",
            ["level_id"] = RoomGenerator._roomCount,
            ["try_number"] = _roomTryNumber,
            ["boosters_used"] = BusterManager.AllUsedBusters,
            ["result"] = "win", // win, fail
            ["time_to_complete"] = (int)_armwrestlingGameTimer.CurrentGameplayTime

        });

        BusterManager.ResetUsedBusterCount();
    }

    public void FailFinishArmwrestlingEventHandler()
    {
        GGAnalytics.Instance.LogEvent("app_level_end", new Dictionary<string, object>
        {
            ["app_name"] = "Hasbik armwrestling",
            ["app_version"] = "0.1",
            ["app_session_id"] = "0.1",
            ["meta_version"] = "1.3.9",
            ["level_id"] = RoomGenerator._roomCount,
            ["try_number"] = _roomTryNumber,
            ["boosters_used"] = BusterManager.AllUsedBusters,
            ["result"] = "fail", // win, fail
            ["time_to_complete"] = (int)_armwrestlingGameTimer.CurrentGameplayTime

        });

        BusterManager.ResetUsedBusterCount();
    }

    private void OnDestroy()
    {
        _eventBus.OnRoomCompleted -= AddTryNumer;
        _eventBus.OnQTECompleted -= ResetTryNumbers;
        _eventBus.OnQTEFailed -= FailFinishArmwrestlingEventHandler;
    }
}
