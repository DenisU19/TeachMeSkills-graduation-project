using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GreenGrey.Analytics;

public class AddStrengthEventHandler : MonoBehaviour
{
    [SerializeField]
    private StrengthCounter _strengthCounter;

    public void AddStrengthEventhandler(string exerciserType, int strengthCount)
    {
        GGAnalytics.Instance.LogEvent("app_player_exp", new Dictionary<string, object>
        {
            ["app_name"] = "Hasbik armwrestling",
            ["app_version"] = "0.1",
            ["app_session_id"] = "0.1",
            ["meta_version"] = "1.3.9",
            ["exp_source"] = exerciserType, // leg or arm exercise
            ["player_level"] = _strengthCounter.CurrentStrengthLevel,
            ["exp_amount"] = strengthCount
        });
    }
}



