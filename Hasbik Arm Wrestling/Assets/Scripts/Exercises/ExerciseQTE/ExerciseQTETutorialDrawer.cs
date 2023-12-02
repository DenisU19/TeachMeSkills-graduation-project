using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseQTETutorialDrawer : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private GameObject _tutorialAnimation;
    [SerializeField]
    private TutorialCompleteEventHandler _tutorialCompleteHandler;

    private void OnEnable()
    {
        _tutorialAnimation.SetActive(true);
        _eventBus.OnPlayerStartTrace += HideTutorialAnimation;
    }

    public void HideTutorialAnimation()
    {
        if(ExerciseQTESpawner.isFirstSpawn == true)
        {
            _tutorialCompleteHandler.CompleteTutorialEventHandler("start exercise", 2);

            ExerciseQTESpawner.isFirstSpawn = false;
        }

        _tutorialAnimation.SetActive(false);
    }

    private void OnDisable()
    {
        _eventBus.OnPlayerStartTrace -= HideTutorialAnimation;
    }

}
