using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainGameTutorial;
    [SerializeField]
    private TutorialCompleteEventHandler _tutorialCompleteHandler;

    private void Awake()
    {
        _mainGameTutorial.SetActive(true);
    }

    private void Update()
    {
        if (PlayerInputHandler._isJoystickTouch == true)
        {
            _mainGameTutorial.SetActive(false);

            _tutorialCompleteHandler.CompleteTutorialEventHandler("start game", 1);

            enabled = false;
        }
    }
}
