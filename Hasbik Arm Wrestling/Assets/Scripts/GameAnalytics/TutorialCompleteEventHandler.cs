using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GreenGrey.Analytics;

public class TutorialCompleteEventHandler : MonoBehaviour
{
    public void CompleteTutorialEventHandler(string tutorialStepName,int tutorialStepNumber)
    {
        GGAnalytics.Instance.LogEvent("app_tutorial", new Dictionary<string, object>
        {
            ["app_name"] = "Hasbik armwrestling",
            ["app_version"] = "0.1",
            ["app_session_id"] = "0.1",
            ["meta_version"] = "1.3.9",
            ["tutorial_step_id"] = tutorialStepName, // start game, start exercise, start armwrestling 
            ["tutorial_step_num"] = tutorialStepNumber // 1, 2, 3
        });
    }
}
