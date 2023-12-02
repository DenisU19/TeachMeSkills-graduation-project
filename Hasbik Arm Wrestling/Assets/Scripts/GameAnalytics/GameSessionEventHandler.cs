using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GreenGrey.Analytics;

public class GameSessionEventHandler : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private BusterManager _sodaBusterManager;
    [SerializeField]
    private BusterManager _barBusterManager;
    [SerializeField]
    private BusterManager _energeticBusterManager;

    private void Awake()
    {
        _eventBus.OnPlayerDataLoaded += StartSessionGameHandler;
    }
    public void StartSessionGameHandler()
    {
        GGAnalytics.Instance.LogEvent("app_open", new Dictionary<string, object>
        {
            ["app_name"] = "Hasbik armwrestling",
            ["app_version"] = "0.1",
            ["app_session_id"] = "0.1",
            ["meta_version"] = "1.3.9",
            ["gold_count"] = MoneyCounter.GoldCount,
            ["soda_buster_cound"] = _sodaBusterManager.CurrentBusterCount,
            ["bar_buster_count"] = _barBusterManager.CurrentBusterCount,
            ["energetic_buster_count"] = _energeticBusterManager.CurrentBusterCount
        });
    }

    public void EndSessionGameHandler()
    {
        GGAnalytics.Instance.LogEvent("app_close", new Dictionary<string, object>
        {
            ["app_name"] = "Hasbik armwrestling",
            ["app_version"] = "0.1",
            ["app_session_id"] = "0.1",
            ["meta_version"] = "1.3.9",
            ["gold_count"] = MoneyCounter.GoldCount,
            ["soda_buster_cound"] = _sodaBusterManager.CurrentBusterCount,
            ["bar_buster_count"] = _barBusterManager.CurrentBusterCount,
            ["energetic_buster_count"] = _energeticBusterManager.CurrentBusterCount
        });
    }

    private void OnApplicationQuit()
    {
        EndSessionGameHandler();
    }

    private void OnDestroy()
    {
        _eventBus.OnPlayerDataLoaded -= StartSessionGameHandler;
    }
}
