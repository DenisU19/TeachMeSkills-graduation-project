using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GreenGrey.Analytics;

public class ArmwrestlingStartEventHandler : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;

    private int _roomTryNumber;

    private void Awake()
    {
        _eventBus.OnRoomCompleted += AddTryNumer;
        _eventBus.OnQTECompleted += ResetTryNumbers;
    }

    public void AddTryNumer()
    {
        _roomTryNumber++;

        StartArmwrestlingEventHandler();
    }

    public void ResetTryNumbers()
    {
        _roomTryNumber = 0;
    }
    public void StartArmwrestlingEventHandler()
    {
        GGAnalytics.Instance.LogEvent("app_level_start", new Dictionary<string, object>
        {
            ["app_name"] = "Hasbik armwrestling",
            ["app_version"] = "0.1",
            ["app_session_id"] = "0.1",
            ["meta_version"] = "1.3.9",
            ["level_id"] = RoomGenerator._roomCount,
            ["try_number"] = _roomTryNumber
        });
    }

    private void OnDestroy()
    {
        _eventBus.OnRoomCompleted -= AddTryNumer;
        _eventBus.OnQTECompleted -= ResetTryNumbers;
    }
}
